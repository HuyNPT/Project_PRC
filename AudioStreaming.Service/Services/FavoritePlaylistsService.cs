using AudioStreaming.Data.UnitOfWork;
using System;

namespace AudioStreaming.Services.Services
{
    public class FavoritePlaylistsService : IFavoritePlaylistsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FavoritePlaylistsService(IUnitOfWork unitOfWork)
        {          
            _unitOfWork = unitOfWork;
        }

        public bool DeleteFavoritePlaylists(Guid PlaylistId, Guid AccountId)
        {
            try
            {
                var rs = _unitOfWork.Repository<Data.Entity.FavoritePlaylist>()
                    .Find(x => x.AccountId == AccountId && x.PlaylistId == PlaylistId);
                _unitOfWork.Repository<Data.Entity.FavoritePlaylist>().HardDelete(rs.Id);
                _unitOfWork.Commit();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
            
        }
      

        public bool PostFavoritePlaylists(Guid PlaylistId, Guid AccountId)
        {
            try
            {
                _unitOfWork.Repository<Data.Entity.FavoritePlaylist>().Insert(
                    new Data.Entity.FavoritePlaylist()
                    {
                        Id = Guid.NewGuid(),
                        PlaylistId = PlaylistId,
                        AccountId = AccountId
                    }
                 );
                _unitOfWork.Commit();
                
                return true;
            }catch(Exception e)
            {
                return false;
            }
            

        }
    }
}
