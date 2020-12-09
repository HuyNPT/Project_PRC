using System;

namespace AudioStreaming.Services.Services
{
    public interface IFavoritePlaylistsService
    {
        bool PostFavoritePlaylists(Guid PlaylistId, Guid AccountId);
        bool DeleteFavoritePlaylists(Guid PlaylistId, Guid AccountId);
    }
}
