using AudioStreaming.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface IPlaylistService
    {
        IList<DTO.Playlist> GetPlayList(bool isSort, bool isDecending, bool isPaging, int page, int pageLimitItem, string orderBy, string searchKey);
        IList<UserFavoritePlaylist> GetUserFavoritePlayList(Guid userId);
        Task<string> PutPlaylist(Guid playlistId, DTO.Playlist playlist, Guid ownerCode);
        Task<IList<DTO.PlaylistWebAdmin>> GetPlayListWebAdmin(Enum SortType, bool isPaging, int page, int pageLimitItem, string searchKey, Guid OwnerCode);

        Task<bool> PostPlayListWebAdmin(string Name, int DateFilter, string ImageUrl, Guid BrandId, List<Guid> Category,Guid UserId);

        Task<string> DeletePlayListWebAdmin(Guid PlaylistId, Guid OwnerCode);

        Task<int> GetPlaylistCount(Guid OwnerCode);

        Task<DTO.PlaylistWebAdmin> GetPlaylistById(Guid PlaylistId);

        Task<string> PutPlayListWebAdmin(Guid PlaylistId,string Name, int DateFilter, string ImageUrl, List<Guid> Category, Guid UserId, Guid OwnerCode);

    }
}
