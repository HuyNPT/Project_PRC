using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AudioStreaming.Data.Entity;

namespace AudioStreaming.Services.Services
{
    public interface IPlaylistDetailsService
    {
        Task<List<PlaylistDetail>> GetDetailsPlaylistByID(Guid PlaylistId);
        Task<bool> DeletePlaylistDetail(Guid PlaylistId, Guid MediaId);
        Task<bool> PostPlaylistDetail(Guid PlaylistId, Guid MediaId);
            
    }
}
