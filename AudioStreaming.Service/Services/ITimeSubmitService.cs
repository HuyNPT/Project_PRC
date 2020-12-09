using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface ITimeSubmitService
    {
        Task<Data.Entity.TimeSubmit> GetTimeSubmit(Guid UserId, Guid StoreID);
    }
}
