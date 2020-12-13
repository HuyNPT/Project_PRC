using AudioStreaming.WebAdminAS.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public interface IUsersServices
    {
        Task<string> Authenticate(String token);
    }
}
