using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using OneStopApp.Interface;

namespace OneStopApp.Service
{
    public class EmailDomainService : IEmailDomainService
    {

        public EmailDomainService()
        {
        }

        public void SendEmail(IList<string> to, IList<string> cc, string subject, string message, List<Attachment> attachments)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The CC.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="attachments">The attachments.</param>
    }
}
