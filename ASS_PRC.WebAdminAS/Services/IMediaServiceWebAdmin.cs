using AudioStreaming.WebAdminAS.Models.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services
{
    public interface IMediaServiceWebAdmin
    {
        Task<List<MediaModel>> GetMedia(string playlistID,int page);
        Task<bool> PostMedia(string MediaName, string URl, string ImageUrl, string Author, string Singer, int Type, List<Guid> Category, string jwt, string PlaylistID,string FileName);
        Task<bool> DeleteMedia(string MediaID, string jwt);

        Task<MediaDTO> GetAllMedia(int page);
        Task<int> GetMediaCount();

        Task<string> PutMedia(string MediaId, string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, string FileName, string jwt);

        Task<MediaModel> GetMediaByID(string MediaId);
    }
}
