using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.IdentityServer.API.Models;
using ZeroStack.IdentityServer.API.Models.AccountViewModels;
using ZeroStack.IdentityServer.API.Services;

namespace ZeroStack.IdentityServer.API.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IStringLocalizerFactory _localizerFactory;

        private readonly IDistributedCache _distributedCache;
        private readonly ISmsSender _smsSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory, IIdentityServerInteractionService interactionService, ApplicationDbContext dbContext, IStringLocalizerFactory localizerFactory, IDistributedCache distributedCache, ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _interactionService = interactionService;
            _dbContext = dbContext;
            _localizerFactory = localizerFactory;
            _distributedCache = distributedCache;
            _smsSender = smsSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var authorizationRequest = await _interactionService.GetAuthorizationContextAsync(returnUrl);

            LoginViewModel? loginViewModel = null;

            if (authorizationRequest?.Client?.ClientId is not null)
            {
                loginViewModel = new LoginViewModel { UserName = authorizationRequest.LoginHint };
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                ApplicationUser? user = _dbContext.Users.FirstOrDefault(u => u.UserName == model.UserName || u.PhoneNumber == model.UserName);

                if (user is null)
                {
                    ModelState.AddModelError(nameof(user.UserName), "Invalid login user name.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user?.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (_interactionService.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToLocal(returnUrl!);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(u => u.UserName == model.UserName || u.PhoneNumber == model.PhoneNumber))
                {
                    ModelState.AddModelError(string.Empty, "User name or phone number is already in use.");
                    return View(model);
                }

                if (string.IsNullOrWhiteSpace(model.ConfirmedCode) || await _distributedCache.GetStringAsync(model.PhoneNumber) != model.ConfirmedCode)
                {
                    ModelState.AddModelError(nameof(model.ConfirmedCode), "Invalid confirmed code.");
                    return View(model);
                }

                var user = new ApplicationUser { UserName = model.UserName, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);

                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                        // Update any authentication tokens as well
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendCode([System.ComponentModel.DataAnnotations.Phone] string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Generate the token and send it

            string code = _distributedCache.GetString(phoneNumber);

            if (string.IsNullOrWhiteSpace(code))
            {
                Random random = new((int)DateTime.Now.Ticks);
                code = random.Next(100000, 999999).ToString();
            }

            await _distributedCache.SetStringAsync(phoneNumber, code, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            await _smsSender.SendSmsAsync(phoneNumber, code);

            return Ok();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [AcceptVerbs("GET", "POST"), AllowAnonymous]
        public IActionResult VerifyPhoneNumber(string phoneNumber)
        {
            var _localizer = _localizerFactory.Create(typeof(RegisterViewModel));

            if (_dbContext.Users.Any(u => u.PhoneNumber == phoneNumber))
            {
                return Json(_localizer["Phone number is already in use."].Value);
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST"), AllowAnonymous]
        public IActionResult VerifyUserName(string userName)
        {
            var _localizer = _localizerFactory.Create(typeof(RegisterViewModel));

            if (_dbContext.Users.Any(u => u.UserName == userName))
            {
                return Json(_localizer["User name is already in use."].Value);
            }

            return Json(true);
        }

    }
}
