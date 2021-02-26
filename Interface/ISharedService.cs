using System.Threading.Tasks;

namespace onestopapp_api.Interface
{
    public interface ISharedService
    {
        Task<bool> SendEmail(string Subject, string Recepients);
        bool IsClientValid(string ClientId);
    }
}