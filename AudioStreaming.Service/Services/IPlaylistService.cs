using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public interface IPlaylistService
    {       
        Task<IList<DTO.PlaylistWebAdmin>> GetPlayListWebAdmin(Enum SortType, bool isPaging, int page, int pageLimitItem, string searchKey, Guid OwnerCode);

        Task<bool> PostPlayListWebAdmin(string Name, int DateFilter, string ImageUrl, Guid BrandId, List<Guid> Category,Guid UserId);

        Task<string> DeletePlayListWebAdmin(Guid PlaylistId, Guid OwnerCode);

        Task<int> GetPlaylistCount(Guid OwnerCode);

        Task<DTO.PlaylistWebAdmin> GetPlaylistById(Guid PlaylistId);

        Task<string> PutPlayListWebAdmin(Guid PlaylistId,string Name, int DateFilter, string ImageUrl, List<Guid> Category, Guid UserId, Guid OwnerCode);

    }
}
