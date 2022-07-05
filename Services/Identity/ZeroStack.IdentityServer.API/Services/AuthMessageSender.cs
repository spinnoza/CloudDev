using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;


namespace ZeroStack.IdentityServer.API.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<AuthMessageSender> _logger;

        public AuthMessageSender(IHttpClientFactory clientFactory, ILogger<AuthMessageSender> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using SmtpClient smtpClient = new("smtp.qq.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("admin@zero.com", "<password>"),
                Port = 587,
                EnableSsl = true
            };

            MailMessage mailMessage = new()
            {
                From = new MailAddress("admin@zero.com", "零度框架")
            };

            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendSmsAsync(string number, string message)
        {
            string requestUri = "https://dysmsapi.aliyuncs.com";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var httpClient = _clientFactory.CreateClient("aliyun");

            request.Options.TryAdd("RegionId", "cn-hangzhou");
            request.Options.TryAdd("Version", "2017-05-25");
            request.Options.TryAdd("Action", "SendSms");
            request.Options.TryAdd("PhoneNumbers", number);
            request.Options.TryAdd("SignName", "<零度框架>");
            request.Options.TryAdd("TemplateParam", new { code = message });
            request.Options.TryAdd("TemplateCode", "SMS_123456789");

            var response = await httpClient.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(result);
            }
            else
            {
                _logger.LogWarning(result);
            }
        }
    }
}
