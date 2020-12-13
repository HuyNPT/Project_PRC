using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public interface IAccountService
    {      
        Task<string> AuthenticateWebAdminAsync(string idToken);
    }
}
