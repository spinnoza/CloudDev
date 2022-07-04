using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.IdentityServer.API.Models;
using ZeroStack.IdentityServer.API.Models.AccountViewModels;

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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory, IIdentityServerInteractionService interactionService, ApplicationDbContext dbContext, IStringLocalizerFactory localizerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _interactionService = interactionService;
            _dbContext = dbContext;
            _localizerFactory = localizerFactory;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendCode([System.ComponentModel.DataAnnotations.Phone] string phoneNumber)
        {
            if (phoneNumber is null)
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            return await Task.FromResult(new OkResult());
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
