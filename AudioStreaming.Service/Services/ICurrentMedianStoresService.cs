using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface ICurrentMedianStoresService
    {
        IList<DTO.CurrentMediaInStoresDTO> GetAllCurrentMediaInStores();
        IList<DTO.CurrentMediaInStoresDTO> GetByStoreID(Guid StoreID);

        Task<String> PutCurrentMediaInStores(Guid storeID, Guid playlistID, Guid mediaID, DateTime timeStart , DateTime timeEnd);

    }
}
