using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ZeroStack.IdentityServer.API.Models;

namespace ZeroStack.IdentityServer.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;

        private readonly IWebHostEnvironment _environment;

        public HomeController(IIdentityServerInteractionService interactionService, IWebHostEnvironment environment)
        {
            _interactionService = interactionService;
            _environment = environment;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            ErrorViewModel errorViewModel = new();

            // retrieve error details from identityserver
            ErrorMessage errorMessage = await _interactionService.GetErrorContextAsync(errorId);

            if (errorMessage is not null)
            {
                errorViewModel.Error = errorMessage;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    errorMessage.ErrorDescription = null;
                }
            }

            return View("Error", errorViewModel);
        }
    }
}
