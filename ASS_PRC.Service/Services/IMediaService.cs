using ASS_PRC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public interface IMediaService
    {
        /*IList<DTO.Media> GetMediaList(bool isSort, bool isDecending, bool isPaging, int page, int pageLimitItem);*/
        Task<IList<DTO.Media>> GetPlayListDetails(Guid PlaylistID, Enum SortType, bool isPaging, int page, int pageLimitItem, Enum Type);
        Task<MediaDTO> GetMedia(Enum SortType, bool isPaging, int page, int pageLimitItem, Enum Type, string SearchKey, string OrderBy, string CategoryName);

        Task<bool> PostMedia(string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, Guid UserId, Guid PlaylistID,string FileName, Guid OwnerCode);
        Task<bool> DeleteMediaWebAdmin(Guid MediaId,Guid OwnerCode);

        Task<int> GetMediaCount();
        Task<string> PutMedia(Guid MediaId,string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, Guid UserId, string FileName, Guid OwnerCode);

        Task<Media> GetMediaByID(Guid MediaID);
    }
}
