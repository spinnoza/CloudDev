namespace ZeroStack.IdentityServer.API.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IdentityServer4.Models.ErrorMessage? Error { get; set; }
    }
}
