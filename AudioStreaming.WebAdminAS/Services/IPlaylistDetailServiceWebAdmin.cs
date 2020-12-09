using AudioStreaming.WebAdminAS.Models.PlaylistDetail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public interface IPlaylistDetailServiceWebAdmin
    {
        Task<List<PlaylistDetailcs>> GetDetailsPlaylistByID(string PlaylistId);
        Task<bool> DeletePlaylistDetail(string PlaylistId, string MediaId, string jwt);
        Task<bool> PostPlaylistDetail(string PlaylistId, string MediaId, string jwt);
    }
}
