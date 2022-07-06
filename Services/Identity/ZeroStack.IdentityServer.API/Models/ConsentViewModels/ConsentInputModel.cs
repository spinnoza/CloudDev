using System.Collections.Generic;

namespace ZeroStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ConsentInputModel
    {
        public string Button { get; set; } = null!;

        public IEnumerable<string> ScopesConsented { get; set; } = null!;

        public bool RememberConsent { get; set; }

        public string ReturnUrl { get; set; } = null!;

        public string? Description { get; set; }
    }
}