using AudioStreaming.Data.Entity;
using AudioStreaming.Data.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public class TimeSubmitService : ITimeSubmitService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TimeSubmitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TimeSubmit> GetTimeSubmit(Guid UserId, Guid StoreID)
        {
            var timeSubmit = _unitOfWork.Repository<Data.Entity.TimeSubmit>()
                .Find(x => x.UserId == UserId & x.StoreId == StoreID);
            if(timeSubmit == null)
            {
                return null;
            }
            var timeSubmitResponse = new Data.Entity.TimeSubmit()
            {
                Id = timeSubmit.Id,
                UserId = timeSubmit.UserId,
                TimeSubmit1 = timeSubmit.TimeSubmit1,
                StoreId = timeSubmit.StoreId
            };
            return timeSubmitResponse;
        }
    }
}
