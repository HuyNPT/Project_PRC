using AudioStreaming.Data.Context;
using AudioStreaming.Data.UnitOfWork;
using AudioStreaming.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public class CurrentMediaInStoresService : ICurrentMedianStoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AudioStreamingContext _audioStreamingContext;
        public CurrentMediaInStoresService(IUnitOfWork unitOfWork, AudioStreamingContext audioStreamingContext)
        {
            _unitOfWork = unitOfWork;
            _audioStreamingContext = audioStreamingContext;
        }

        public IList<CurrentMediaInStoresDTO> GetAllCurrentMediaInStores()
        {
            IList<CurrentMediaInStoresDTO> currentMedia = new List<CurrentMediaInStoresDTO>();
            var currentMediaInStore = _unitOfWork.Repository<Data.Entity.CurrentMediaInStore>().GetAll().AsQueryable();
            foreach (var item in currentMediaInStore)
            {
                currentMedia.Add(new DTO.CurrentMediaInStoresDTO()
                {
                    Id=item.Id,
                    MediaId=item.MediaId,
                    PlaylistId=item.PlaylistId,
                    StoreId=item.StoreId,
                    TimeStart=item.TimeStart,
                    TimeEnd=item.TimeEnd,
                    TimeToPlay=item.TimeToPlay
                });
            }
            return currentMedia;
        }

        public IList<CurrentMediaInStoresDTO> GetByStoreID(Guid StoreID)
        {
            IList<CurrentMediaInStoresDTO> currentMedia = new List<CurrentMediaInStoresDTO>();
            var currentMediaInStore = _unitOfWork.Repository<Data.Entity.CurrentMediaInStore>()
                .GetAll().Where(x=>x.StoreId==StoreID)
                .AsQueryable();
            foreach (var item in currentMediaInStore)
            {
                currentMedia.Add(new DTO.CurrentMediaInStoresDTO()
                {
                    Id = item.Id,
                    MediaId = item.MediaId,
                    PlaylistId = item.PlaylistId,
                    StoreId = item.StoreId,
                    TimeStart = item.TimeStart,
                    TimeEnd = item.TimeEnd,
                    TimeToPlay = item.TimeToPlay
                });
            }
            return currentMedia;
        }

        public async Task<string> PutCurrentMediaInStores(Guid storeID, Guid playlistID, Guid mediaID, DateTime timeStart, DateTime timeEnd)
        {
            var currentMediaInStoreEntity = _unitOfWork.Repository<Data.Entity.CurrentMediaInStore>().Find(x => x.StoreId.Equals(storeID));
            if(currentMediaInStoreEntity == null)
            {
                _unitOfWork.Repository<Data.Entity.CurrentMediaInStore>().Insert(
                    new Data.Entity.CurrentMediaInStore()
                    {
                        Id = Guid.NewGuid(),
                        MediaId = mediaID,
                        PlaylistId = playlistID,
                        StoreId = storeID,
                        TimeStart = timeStart,
                        TimeEnd = timeEnd
                    }
                 );
            }
            else
            {
                currentMediaInStoreEntity.PlaylistId = playlistID;
                currentMediaInStoreEntity.MediaId = mediaID;
                currentMediaInStoreEntity.TimeStart = timeStart;
                currentMediaInStoreEntity.TimeEnd = timeEnd;
                _unitOfWork.Repository<Data.Entity.CurrentMediaInStore>()
                    .Update(currentMediaInStoreEntity,currentMediaInStoreEntity.Id );
            }
            _unitOfWork.Commit();
            return "Success";
        }
    }
}
