using IdentityServer4.Models;

namespace ZeroStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;

        public string RedirectUri { get; set; } = null!;

        public Client? Client { get; set; }

        public bool ShowView => ViewModel != null;

        public ConsentViewModel? ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;

        public string ValidationError { get; set; } = null!;
    }
}
