using System.Collections.Generic;
using System.Net.Mail;

namespace OneStopApp.Interface
{
    public interface IEmailDomainService
    {
        void SendEmail(IList<string> to, IList<string> cc, string subject, string message, List<Attachment> attachments);
    }
}