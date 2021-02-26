using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using onestopapp_api.Interface;
using OneStopApp_Api.EntityFramework.Data;
using OneStopApp_Api.Enums;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace onestopapp_api.Service
{
    public class SharedService : ISharedService
    {
        private readonly OsaDbContext _context;
        public SharedService(OsaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendEmail(string Subject, string Recepient)
        {
            var Result = false;
            var client = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@onestopapp.com", "One Stop"),
                Subject = Subject,
                HtmlContent = "html tags"
            };
            msg.AddTo(new EmailAddress(Recepient));
            msg.SetClickTracking(false, false);
            var Response = await client.SendEmailAsync(msg);
            return Result = Response.IsSuccessStatusCode;
        }

        public bool IsClientValid(string ClientId)
        {
            var Client = _context.ClientValidations.Where(cv => cv.Name == "ClientId").First();
            return Client.Value == ClientId;
        }
    }
}