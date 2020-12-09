using AudioStreaming.Services.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public interface IPlaylistsInStoreService
    {
        Task<IList<PlaylistInStoreDTO>> GetPlaylistsInStores(Guid StoreId, Enum SortType, bool isPaging, int page, int pageLimitItem);

        Task<String> PutFavoritePlaylists(Guid UserID, List<Guid> List, bool IsVip, Guid StoreId);

        Task<PlaylistInStoreDTO> GetTopPlaylistInStore(Guid StoreId);

        Task AddCachePlaylistInStoreToDatabase();
        Task AddTodayPlaylistInStore();
    }
}
