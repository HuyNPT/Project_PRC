using AudioStreaming.Data.Entity;
using AudioStreaming.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> AuthenticateAsync(string idToken, string fcmToken);
        Task<IList<DTO.Account>> GetAll();
        Task<AuthenticateResponse> AuthenticateByIdAsync(Guid Id);

        Task<string> AuthenticateWebAdminAsync(string idToken);


    }
}
