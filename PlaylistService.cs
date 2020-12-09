using AudioStreaming.Data.Entity;
using AudioStreaming.Data.UnitOfWork;
using AudioStreaming.Services.DTO;
using AudioStreaming.Services.Helpers;
using AudioStreaming.Services.Responses;
using FirebaseAdmin.Messaging;
using Google.Apis.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Services.Services
{
    public class PlaylistService : IPlaylistService
    {
        private const int PageLimit = 15;
        private readonly IUnitOfWork _unitOfWork;

        public PlaylistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> DeletePlayListWebAdmin(Guid PlaylistId, Guid OwnerCode)
        {
            var playlist = _unitOfWork.Repository<Data.Entity.Playlist>().Find(x => x.Id == PlaylistId & x.BrandId == OwnerCode);
            if(playlist == null)
            {
                return "Unauthorized";
            }
            /*var categoryPl=_unitOfWork.Repository<Data.Entity.CategoryPlaylist>().FindAll(x => x.PlaylistId == PlaylistId);
            foreach(var item in categoryPl)
            {
                _unitOfWork.Repository<Data.Entity.CategoryPlaylist>().HardDelete(item.Id);
            }
            
            var playlistDetails = _unitOfWork.Repository<Data.Entity.PlaylistDetail>().FindAll(x => x.PlaylistId == PlaylistId);
            foreach (var item in playlistDetails)
            {
                _unitOfWork.Repository<Data.Entity.PlaylistDetail>().HardDelete(item.Id);
            }
            var favoritePlaylist = _unitOfWork.Repository<Data.Entity.FavoritePlaylist>().FindAll(x => x.PlaylistId == PlaylistId);
            foreach (var item in favoritePlaylist)
            {
                _unitOfWork.Repository<Data.Entity.FavoritePlaylist>().HardDelete(item.Id);
            }*/



            playlist.IsDelete = true;
            try
            {
                _unitOfWork.Commit();
                return "Delete Successfully";
            }catch(Exception e)
            {
                return "Delete fail";
            }
           
        }

        public IList<DTO.Playlist> GetPlayList(bool isSort, bool isDecending, bool isPaging, int page, int pageLimitItem, string orderBy, string searchKey) 
        {
            var playlistEntities = _unitOfWork.Repository<Data.Entity.Playlist>()
                .FindAll(p => p.IsDelete == false && p.PlaylistName.StartsWith(searchKey==null?"":searchKey));
            if (isSort)
            {
                if(typeof(Data.Entity.Playlist).GetProperty(orderBy) == null)
                {
                    orderBy = "Id";
                }
                if (isDecending)
                {
                    playlistEntities= playlistEntities.OrderByDescending(x => typeof(Data.Entity.Playlist).GetProperty(orderBy));
                }else playlistEntities =  playlistEntities.OrderBy(x => typeof(Data.Entity.Playlist).GetProperty(orderBy));
            }
            if (isPaging)
            {
               playlistEntities= playlistEntities.Skip(page * pageLimitItem).Take(pageLimitItem);
            }
            IList<DTO.Playlist> playlist = new List<DTO.Playlist>();
            foreach (var item in playlistEntities)
            {
                playlist.Add(new DTO.Playlist()
                {
                    Id =item.Id,             
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    DateFillter = item.DateFillter,
                    ImageUrl = item.ImageUrl,
                    IsDelete = item.IsDelete,
                    ModifyBy = item.ModifyBy,
                    ModifyDate = item.ModifyDate,
                    PlaylistName = item.PlaylistName,
                    TimePlayed = item.TimePlayed,
                });
            }


            return playlist;


        }

        public async Task<int> GetPlaylistCount(Guid OwnerCode)
        {
            var count =_unitOfWork.Repository<Data.Entity.Playlist>().FindAll(x => x.IsDelete == false & x.BrandId == OwnerCode).Count();
            return count;
        }

        public async Task<IList<PlaylistWebAdmin>> GetPlayListWebAdmin(Enum SortType, bool isPaging, int page, int pageLimitItem, string searchKey, Guid OwnerCode)
        {
            List<PlaylistWebAdmin> listPlaylist = new List<PlaylistWebAdmin>();
            var playlistEntities = _unitOfWork.Repository<Data.Entity.Playlist>().GetAll().Where(p => p.IsDelete == false && p.BrandId==OwnerCode &&p.PlaylistName.StartsWith(searchKey == null ? "" : searchKey)).Include(x => x.Brand)
            .Include(a => a.CategoryPlaylist).ThenInclude(d => d.Category).AsQueryable();
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
                    playlistEntities = playlistEntities.OrderByDescending(x => x.PlaylistName);
                }
                else
                {
                    playlistEntities = playlistEntities.OrderBy(x => x.PlaylistName);
                }
            }
            if (isPaging)
            {
                playlistEntities = playlistEntities.Skip(page * pageLimitItem).Take(pageLimitItem);
            }
            foreach (var item in playlistEntities)
            {
                List<DTO.CategoryPlaylist> listCategoryPlaylist = new List<DTO.CategoryPlaylist>();
                item.CategoryPlaylist.ToList().ForEach(e =>
                {
                    DTO.CategoryPlaylist tmp = new DTO.CategoryPlaylist();
                    tmp.Id = e.Id;
                    tmp.PlaylistId = e.PlaylistId;
                    tmp.CategoryId = e.CategoryId;
                    tmp.Category = new List<DTO.Category>()
                        {
                            new DTO.Category()
                            {
                                Id=e.Category.Id,
                                CategoryName=e.Category.CategoryName
                            }
                        };
                    listCategoryPlaylist.Add(tmp);
                }
                );
                listPlaylist.Add(new DTO.PlaylistWebAdmin()
                {
                    Id = item.Id,
                    PlaylistName = item.PlaylistName,
                    ModifyDate = item.ModifyDate,
                    CreateDate = item.CreateDate,
                    BrandId = item.BrandId,
                    BrandName=item.Brand.BrandName,
                    DateFillter=item.DateFillter,
                    ImageUrl=item.ImageUrl,
                    IsDelete=item.IsDelete,
                    TimePlayed=item.TimePlayed,
                    CategoryPlaylists = listCategoryPlaylist
                });
            }
            return listPlaylist;
        }

        public IList<UserFavoritePlaylist> GetUserFavoritePlayList(Guid userId)
        {
            IList<UserFavoritePlaylist> userFavoritePlaylists = new List<UserFavoritePlaylist>();


            var playlistEntities = _unitOfWork.Repository<Data.Entity.Playlist>()
                .GetAll().Where(x=>x.IsDelete == false)
                .Include(p => p.FavoritePlaylist)
                .Where(p => p.FavoritePlaylist.Any(f => f.AccountId == userId)).Include(f => f.Brand);
            foreach (var item in playlistEntities)
            {
              
                UserFavoritePlaylist userFavoritePlaylist = new UserFavoritePlaylist()
                {
                    Id = item.Id,                 
                    ImageUrl = item.ImageUrl,
                    PlaylistName = item.PlaylistName,
                    DateFillter = item.DateFillter,
                    BrandName = item.Brand.BrandName
                };
                 userFavoritePlaylists.Add(userFavoritePlaylist);
            }
            return userFavoritePlaylists;
        }

        public async Task<bool> PostPlayListWebAdmin(string Name, int DateFilter, string ImageUrl, Guid BrandId, List<Guid> Category, Guid UserId)
        {
            Guid playlistID = Guid.NewGuid();
            _unitOfWork.Repository<Data.Entity.Playlist>().Insert(
                new Data.Entity.Playlist()
                {
                    Id = playlistID,
                    PlaylistName = Name,
                    CreateBy = UserId,
                    CreateDate = GetTimeNowVN.GetTimeNowVietNam(),
                    ModifyBy = UserId,
                    ModifyDate = GetTimeNowVN.GetTimeNowVietNam(),
                    BrandId = BrandId,
                    DateFillter = DateFilter,
                    ImageUrl = ImageUrl,
                    IsDelete = false,
                    TimePlayed = 0,                   
                }
               );
            _unitOfWork.Commit();
            var catePlaylist = _unitOfWork.Repository<Data.Entity.CategoryPlaylist>();
            Category.ForEach(e =>
            {
                catePlaylist.Insert(new Data.Entity.CategoryPlaylist()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = e,
                    PlaylistId = playlistID
                }
                );
            });
            try
            {
                _unitOfWork.Commit();
                var tmp = _unitOfWork.Repository<Data.Entity.Playlist>().Find(p => p.Id == playlistID);
                if(tmp != null)
                {
                    SendMsg();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            

            return true;
        }

        internal static async Task SendMsg()
        {

            var topic = "TheCoffeeHouse";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Body = "Liked Song",
                    Title = "Liked The Song"
                },
                Data = new Dictionary<string, string>()
                {
                    { "score", "100" },
                    { "time", "0:00" },
                },
                Topic = topic,
                //Token = "fvOB5LollWE:APA91bE63MbCymk1aHrPYxZrNI3dQ2_kZfQfwjv3KqcZ_EC3ZX_itQO1LrcNn5H0howKlNPMuKYK-cWwB-eq7GDq4K02FpN0Cy4FXeMKl9nZOiZq-HfdE3AUALyFDc4rDfQhfc_vOOU9",
            };

            // Send a message to the devices subscribed to the provided topic.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public async Task<string> PutPlaylist(Guid playlistId, DTO.Playlist playlist, Guid ownerCode)
        {
            Data.Entity.Playlist playlistEntity = _unitOfWork.Repository<Data.Entity.Playlist>().GetById(playlistId);
            if (playlistEntity == null) return "Playlist not exist";
            if(!playlistEntity.BrandId.Equals(ownerCode)) return "Your role not permission";
            Data.Entity.Playlist playlistUpdate = new Data.Entity.Playlist
            {
                Id = playlistId,
                BrandId = playlist.BrandId,
                CreateBy = playlist.CreateBy,
                CreateDate = playlist.CreateDate,
                DateFillter = playlist.DateFillter,
                IsDelete = playlist.IsDelete,
                ImageUrl = playlist.ImageUrl,
                PlaylistName = playlist.PlaylistName,
                TimePlayed = playlist.TimePlayed,
                ModifyBy = playlist.ModifyBy,
                ModifyDate = playlist.ModifyDate,
            };
            _unitOfWork.Repository<Data.Entity.Playlist>().Update(playlistUpdate, playlistId);
            _unitOfWork.Commit();
            return "Success";
        }

        public async Task<PlaylistWebAdmin> GetPlaylistById(Guid PlaylistId)
        {
            var rs =_unitOfWork.Repository<Data.Entity.Playlist>().GetAll().Where(p => p.IsDelete == false & p.Id == PlaylistId).Include(x => x.Brand)
            .Include(a => a.CategoryPlaylist).ThenInclude(d => d.Category).FirstOrDefault();
            if(rs == null)
            {
                return null;
            }
            List<DTO.CategoryPlaylist> listCategoryPlaylist = new List<DTO.CategoryPlaylist>();
            rs.CategoryPlaylist.ToList().ForEach(e =>
            {
                DTO.CategoryPlaylist tmp = new DTO.CategoryPlaylist();
                tmp.Id = e.Id;
                tmp.PlaylistId = e.PlaylistId;
                tmp.CategoryId = e.CategoryId;
                tmp.Category = new List<DTO.Category>()
                        {
                            new DTO.Category()
                            {
                                Id=e.Category.Id,
                                CategoryName=e.Category.CategoryName
                            }
                        };
                listCategoryPlaylist.Add(tmp);
            }
            );
            PlaylistWebAdmin x = new PlaylistWebAdmin()
            {
                Id = rs.Id,
                PlaylistName = rs.PlaylistName,
                ModifyDate = rs.ModifyDate,
                CreateDate = rs.CreateDate,
                BrandId = rs.BrandId,
                BrandName = rs.Brand.BrandName,
                DateFillter = rs.DateFillter,
                ImageUrl = rs.ImageUrl,
                IsDelete = rs.IsDelete,
                TimePlayed = rs.TimePlayed,
                CategoryPlaylists = listCategoryPlaylist

            };
            return x;
        }

        public async Task<string> PutPlayListWebAdmin(Guid PlaylistId, string Name, int DateFilter, string ImageUrl, List<Guid> Category, Guid UserId, Guid OwnerCode)
        {
            var query = _unitOfWork.Repository<Data.Entity.Playlist>().GetById(PlaylistId);
            if (query.BrandId != OwnerCode)
            {
                return "Unauthorized";
            }
            if(query.IsDelete == true)
            {
                return "Fail";
            }
            query.PlaylistName = Name;
            query.DateFillter = DateFilter;
            query.ImageUrl = ImageUrl;
            query.ModifyBy = UserId;
            query.ModifyDate = GetTimeNowVN.GetTimeNowVietNam();

            var categoryPl = _unitOfWork.Repository<Data.Entity.CategoryPlaylist>().FindAll(x => x.PlaylistId == PlaylistId);
            foreach (var item in categoryPl)
            {
                _unitOfWork.Repository<Data.Entity.CategoryPlaylist>().HardDelete(item.Id);
            }

            var catePlaylist = _unitOfWork.Repository<Data.Entity.CategoryPlaylist>();
            Category.ForEach(e =>
            {
                catePlaylist.Insert(new Data.Entity.CategoryPlaylist()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = e,
                    PlaylistId = PlaylistId
                }
                );
            });
            try
            {
                _unitOfWork.Commit();
                return "Success";
            }
            catch (Exception e)
            {
                return "Fail";
            }
        }
    }
}
