using AudioStreaming.Data.UnitOfWork;
using AudioStreaming.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<DTO.Brand>> GetBrands(bool isSort, bool isDecending, bool isPaging, int page, int pageLimitItem, string orderBy, string searchKey)
        {
            var brandEntities = _unitOfWork.Repository<Data.Entity.Brand>().GetAll()
                .Where(p => p.IsDelete == false && p.BrandName.StartsWith(searchKey == null ? "" : searchKey))
                .Include(b => b.Playlist).AsQueryable();
            if (isSort)
            {
                if (typeof(Playlist).GetProperty(orderBy) == null)
                {
                    orderBy = "Id";
                }
                if (isDecending)
                {
                    brandEntities = brandEntities.OrderByDescending(x => typeof(Brand).GetProperty(orderBy));
                }
                else brandEntities = brandEntities.OrderBy(x => typeof(Playlist).GetProperty(orderBy));
            }
            if (isPaging)
            {
                brandEntities = brandEntities.Skip(page * pageLimitItem).Take(pageLimitItem);
            }
            IList<DTO.Brand> brands = new List<DTO.Brand>();
            foreach (var item in brandEntities)
            {
                DTO.Brand brand = new Brand()
                {
                    Id = item.Id,
                    BrandName = item.BrandName,
                    CompanyName = item.CompanyName,
                    Address = item.Address,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                };
                foreach (var itemPlaylist in item.Playlist)
                {
                    if (brand.Playlist == null) brand.Playlist = new List<DTO.Playlist>();
                    brand.Playlist.Add(new DTO.Playlist() {
                        Id = itemPlaylist.Id,
                        CreateBy = itemPlaylist.CreateBy,
                        CreateDate = itemPlaylist.CreateDate,
                        DateFillter = itemPlaylist.DateFillter,
                        ImageUrl = itemPlaylist.ImageUrl,
                        IsDelete = itemPlaylist.IsDelete,
                        ModifyBy = itemPlaylist.ModifyBy,
                        ModifyDate = itemPlaylist.ModifyDate,
                        PlaylistName = itemPlaylist.PlaylistName,
                        TimePlayed = itemPlaylist.TimePlayed,
                    });
                }
                brands.Add(brand);
            }
            return brands;
        }
    }
}
