using ASS_PRC.Data.Context;
using ASS_PRC.Data.UnitOfWork;
using ASS_PRC.Services.DTO;
using ASS_PRC.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public class MediaService : IMediaService
    {
        private const int PageLimit = 15;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PRCContext _audioStreamingContext;

        public MediaService(IUnitOfWork unitOfWork, PRCContext audioStreamingContext)
        {
            _unitOfWork = unitOfWork;
            _audioStreamingContext = audioStreamingContext;
        }

        public async Task<MediaDTO> GetMedia(Enum SortType, bool isPaging, int page, int pageLimitItem, Enum Type, string SearchKey, string OrderBy, string CategoryName)
        {
            List<DTO.Media> media = new List<DTO.Media>();


            var mediaEntities = _unitOfWork.Repository<Data.Entity.Media>()
                .GetAll().Where(x => x.IsDelete == false & x.MusicName.StartsWith(SearchKey == null ? "" : SearchKey))
                .Include(p => p.PlaylistDetail)
                .Include(m => m.CategoryMedia)
                .ThenInclude(c => c.Category)
                .AsQueryable();
            int counter = 0;
            if(mediaEntities != null)
            {
                counter = mediaEntities.Count();
            }
            

            int sortTypeInt = -1;
            try
            {
                sortTypeInt = int.Parse(SortType.ToString());
            }
            catch (Exception e)
            {

            }


            if (sortTypeInt != 0)
            {
                if (SortType.ToString().Equals("SortDesc"))
                {
                    mediaEntities = mediaEntities.OrderByDescending(x => x.MusicName);
                }
                else
                {
                    //mediaEntities = mediaEntities.OrderBy(x => typeof(Data.Entity.Media).GetProperty(OrderBy));
                    mediaEntities = mediaEntities.OrderBy(x => x.MusicName);
                }
            }
            if (isPaging)
            {
                mediaEntities = mediaEntities.Skip(page * pageLimitItem).Take(pageLimitItem);
            }
            int typeMedia = -1;
            try
            {
                typeMedia = int.Parse(Type.ToString());
            }
            catch (Exception e)
            {

            }
            if (typeMedia != 0)
            {
                mediaEntities = mediaEntities.Where(e => Type.ToString().Equals("Music") ? e.Type == 1 : e.Type == 2);
            }
            
            foreach (var item in mediaEntities)
            {
                List<CategoryMedia> listCategoryMedia = new List<CategoryMedia>();
                item.CategoryMedia.ToList().ForEach(e =>
                {
                    if (e.MediaId == item.Id)
                    {
                        CategoryMedia tmp = new CategoryMedia();
                        tmp.Id = e.Id;
                        tmp.MediaId = e.MediaId;
                        tmp.CategoryId = e.CategoryId;
                        tmp.Category = new List<Category>()
                        {
                            new Category()
                            {
                                Id=e.Category.Id,
                                CategoryName=e.Category.CategoryName
                            }
                        };
                        listCategoryMedia.Add(tmp);
                    }
                    

                });
                List<PlaylistDetails> listPlaylistDetails = new List<PlaylistDetails>();
                item.PlaylistDetail.ToList().ForEach(e =>
                {
                    PlaylistDetails temp = new PlaylistDetails();
                    temp.Id = e.Id;
                    temp.PlaylistId = e.PlaylistId;
                    temp.MusicId = e.MediaId;
                    temp.NumericalOrder = e.NumericalOrder;
                    listPlaylistDetails.Add(temp);
                    
                });
                media.Add(new DTO.Media()
                {
                    Id = item.Id,
                    MusicName = item.MusicName,
                    ModifyBy = item.ModifyBy,
                    ModifyDate = item.ModifyDate,
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    IsDelete = item.IsDelete,
                    Url = item.Url,
                    ImageUrl = item.ImageUrl,
                    Author = item.Author,
                    Singer = item.Singer,
                    Type = item.Type,
                    FileName = item.FileName,
                    PlaylistDetails = listPlaylistDetails,
                    CategoryMedia = listCategoryMedia,

                });

            }
            MediaDTO response = new MediaDTO()
            {
                Counter = counter,
                ListMedia = media
            };
            return response;
        }

        public async Task<IList<Media>> GetPlayListDetails(Guid playlistID, Enum SortType, bool isPaging, int page, int pageLimitItem, Enum Type)
        {
            IList<DTO.Media> media = new List<DTO.Media>();


            var mediaEntities = _unitOfWork.Repository<Data.Entity.Media>()
                .GetAll().Where(x => x.IsDelete == false & x.PlaylistDetail.Any(p => p.PlaylistId == playlistID))
                .Include(p => p.PlaylistDetail)
                .Include(m => m.CategoryMedia)
                .ThenInclude(c => c.Category)
                .AsQueryable();

            int sortTypeInt = -1;
            try
            {
                sortTypeInt = int.Parse(SortType.ToString());
            }
            catch (Exception e)
            {

            }


            if (sortTypeInt != 0)
            {
                if (SortType.ToString().Equals("SortDesc"))
                {
                    mediaEntities = mediaEntities.OrderByDescending(x => x.PlaylistDetail.FirstOrDefault(a => a.PlaylistId == playlistID && a.MediaId == x.Id).NumericalOrder);
                }
                else
                {
                    mediaEntities = mediaEntities.OrderBy(x => x.PlaylistDetail.FirstOrDefault(a => a.PlaylistId == playlistID && a.MediaId == x.Id).NumericalOrder);
                }
            }
            if (isPaging)
            {
                mediaEntities = mediaEntities.Skip(page * pageLimitItem).Take(pageLimitItem);
            }
            int typeMedia = -1;
            try
            {
                typeMedia = int.Parse(Type.ToString());
            }
            catch (Exception e)
            {

            }
            if (typeMedia != 0)
            {
                mediaEntities = mediaEntities.Where(e => Type.ToString().Equals("Music") ? e.Type == 1 : e.Type == 2);
            }
            
            foreach (var item in mediaEntities)
            {
                List<CategoryMedia> listCategoryMedia = new List<CategoryMedia>();
                item.CategoryMedia.ToList().ForEach(e =>
                {
                    if (e.MediaId == item.Id)
                    {
                        CategoryMedia tmp = new CategoryMedia();
                        tmp.Id = e.Id;
                        tmp.MediaId = e.MediaId;
                        tmp.CategoryId = e.CategoryId;
                        tmp.Category = new List<Category>()
                        {
                            new Category()
                            {
                                Id=e.Category.Id,
                                CategoryName=e.Category.CategoryName
                            }
                        };
                        listCategoryMedia.Add(tmp);
                    }
                    

                });
                List<PlaylistDetails> listPlaylistDetails = new List<PlaylistDetails>();
                item.PlaylistDetail.ToList().ForEach(e =>
                {
                    PlaylistDetails temp = new PlaylistDetails();
                    temp.Id = e.Id;
                    temp.PlaylistId = e.PlaylistId;
                    temp.MusicId = e.MediaId;
                    temp.NumericalOrder = e.NumericalOrder;
                    listPlaylistDetails.Add(temp);

                });
                media.Add(new DTO.Media()
                {
                    Id = item.Id,
                    MusicName = item.MusicName,
                    ModifyBy = item.ModifyBy,
                    ModifyDate = item.ModifyDate,
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    IsDelete = item.IsDelete,
                    Url = item.Url,
                    ImageUrl = item.ImageUrl,
                    Author = item.Author,
                    Singer = item.Singer,
                    Type = item.Type,
                    FileName = item.FileName,
                    PlaylistDetails = listPlaylistDetails,
                    CategoryMedia =listCategoryMedia,

                });

            }
            return media;
        }

        public async Task<bool> PostMedia(string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, Guid UserId, Guid PlaylistID, string FileName, Guid OwnerCode)
        {
            Guid MediaId = Guid.NewGuid();
            _unitOfWork.Repository<Data.Entity.Media>().Insert(
                new Data.Entity.Media()
                {
                    Id = MediaId,
                    MusicName = MediaName,
                    CreateBy = UserId,
                    CreateDate = GetTimeNowVN.GetTimeNowVietNam(),
                    ModifyBy = UserId,
                    ModifyDate = GetTimeNowVN.GetTimeNowVietNam(),
                    Author=Author,
                    Singer=Singer,
                    ImageUrl = ImaUrl,
                    IsDelete = false,
                    Type = Type,
                    Url = Url,
                    FileName=FileName,
                    OwnerCode = OwnerCode
                }
               );
            _unitOfWork.Commit();
            var catePlaylist = _unitOfWork.Repository<Data.Entity.CategoryMedia>();
            Category.ForEach(e =>
            {
                catePlaylist.Insert(new Data.Entity.CategoryMedia()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = e,
                    MediaId = MediaId
                }
                );
            });
            if(PlaylistID != new Guid())
            {
                int order = 1;
                var playlistDetails = _unitOfWork.Repository<Data.Entity.PlaylistDetail>().FindAll(x => x.PlaylistId == PlaylistID)
                    .OrderByDescending(p => p.NumericalOrder).FirstOrDefault();
                if(playlistDetails != null)
                {
                    order = Int32.Parse((playlistDetails.NumericalOrder + 1).ToString());
                }
                _unitOfWork.Repository<Data.Entity.PlaylistDetail>().Insert(new Data.Entity.PlaylistDetail()
                {
                    Id=Guid.NewGuid(),
                    MediaId = MediaId,
                    NumericalOrder=order,
                    PlaylistId = PlaylistID,                 
                });
            }
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                return false;
            }


            return true;
        }
        public async Task<bool> DeleteMediaWebAdmin(Guid MediaId,Guid OwnerCode)
        {
            var media = _unitOfWork.Repository<Data.Entity.Media>().Find(x => x.Id == MediaId);
            if (media == null)
            {
                return false;
            }
            if(media.OwnerCode != OwnerCode)
            {
                return false;
            }
            /*var query = _unitOfWork.Repository<Data.Entity.PlaylistDetail>().FindAll(x => x.MediaId == MediaId)
                .Include(p => p.Playlist).AsQueryable();
            foreach(var item in query)
            {
                if(item.Playlist.BrandId != OwnerCode)
                {
                    return false;
                }
            }*/
            
            var categoryPl = _unitOfWork.Repository<Data.Entity.CategoryMedia>().FindAll(x => x.MediaId == MediaId);
            foreach (var item in categoryPl)
            {
                _unitOfWork.Repository<Data.Entity.CategoryMedia>().HardDelete(item.Id);
            }

            var playlistDetails = _unitOfWork.Repository<Data.Entity.PlaylistDetail>().FindAll(x => x.MediaId == MediaId);
            foreach (var item in playlistDetails)
            {
                _unitOfWork.Repository<Data.Entity.PlaylistDetail>().HardDelete(item.Id);
            }           
            _unitOfWork.Repository<Data.Entity.Media>().HardDelete(MediaId);
            try
            {
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<int> GetMediaCount()
        {
            var count = _unitOfWork.Repository<Data.Entity.Media>().FindAll(x => x.IsDelete == false).Count();
            return count;
        }

        public async Task<string> PutMedia(Guid MediaId,string MediaName, string Url, string ImaUrl, string Author, string Singer, int Type, List<Guid> Category, Guid UserId, string FileName, Guid OwnerCode)
        {
            
            var query = _unitOfWork.Repository<Data.Entity.Media>().GetById(MediaId);
            if(query.OwnerCode != OwnerCode)
            {
                return "Unauthorized";
            }
            query.MusicName = MediaName;
            query.Url = Url;
            query.ImageUrl = ImaUrl;
            query.Author = Author;
            query.Singer = Singer;
            query.Type = Type;
            query.FileName = FileName;
            query.ModifyBy = UserId;
            query.ModifyDate = GetTimeNowVN.GetTimeNowVietNam();

            var categoryPl = _unitOfWork.Repository<Data.Entity.CategoryMedia>().FindAll(x => x.MediaId == MediaId);
            foreach (var item in categoryPl)
            {
                _unitOfWork.Repository<Data.Entity.CategoryMedia>().HardDelete(item.Id);
            }

            var catePlaylist = _unitOfWork.Repository<Data.Entity.CategoryMedia>();
            Category.ForEach(e =>
            {
                catePlaylist.Insert(new Data.Entity.CategoryMedia()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = e,
                    MediaId = MediaId
                }
                );
            });
            try
            {
                _unitOfWork.Commit();
                return "Success";
            }catch(Exception e)
            {
                return "Fail";
            }
        }

        public async Task<Media> GetMediaByID(Guid MediaID)
        {
            var rs = _unitOfWork.Repository<Data.Entity.Media>().GetById(MediaID);
            if(rs == null)
            {
                return null;
            }
            List<CategoryMedia> listCategoryMedia = new List<CategoryMedia>();
            if(rs.CategoryMedia != null)
            {
                rs.CategoryMedia.ToList().ForEach(e =>
                {
                    CategoryMedia tmp = new CategoryMedia();
                    tmp.Id = e.Id;
                    tmp.MediaId = e.MediaId;
                    tmp.CategoryId = e.CategoryId;
                    tmp.Category = new List<Category>()
                        {
                            new Category()
                            {
                                Id=e.Category.Id,
                                CategoryName=e.Category.CategoryName
                            }
                        };
                    listCategoryMedia.Add(tmp);
                });
            }
            
            DTO.Media x = new DTO.Media()
            {
                Id = rs.Id,
                MusicName = rs.MusicName,
                ModifyBy = rs.ModifyBy,
                ModifyDate = rs.ModifyDate,
                CreateBy = rs.CreateBy,
                CreateDate = rs.CreateDate,
                IsDelete = rs.IsDelete,
                Url = rs.Url,
                ImageUrl = rs.ImageUrl,
                Author = rs.Author,
                Singer = rs.Singer,
                Type = rs.Type,
                FileName = rs.FileName,
                PlaylistDetails = null,
                CategoryMedia = listCategoryMedia,
            };
            return x;

        }
    }
    
}