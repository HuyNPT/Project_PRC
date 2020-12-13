using ASS_PRC.Data.UnitOfWork;
using ASS_PRC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public class StoresService : IStoresService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoresService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DTO.Store> GetStoreById(Guid Id)
        {
            var storeEntity = _unitOfWork.Repository<Data.Entity.Store>().GetById(Id);
            var storeResponse = new DTO.Store()
            {
                Id = storeEntity.Id,
                StoreName = storeEntity.StoreName,
                Address = storeEntity.Address,
                BrandId = storeEntity.BrandId,
                IsDelete = storeEntity.IsDelete,
                Wifi = storeEntity.Wifi,
                PassWifi = storeEntity.PassWifi,
                ImageUrl = storeEntity.Brand.ImageUrl,
                BrandName = storeEntity.Brand.BrandName
            };
            return storeResponse;
        }
    }
}
