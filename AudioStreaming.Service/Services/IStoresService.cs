using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface IStoresService
    {
        Task<DTO.Store> GetStoreById(Guid Id);
    }
}
