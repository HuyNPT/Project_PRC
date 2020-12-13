using ASS_PRC.Data.Entity;
using ASS_PRC.Data.UnitOfWork;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ASS_PRC.Services.Services
{
    public class PlaylistDetailsService : IPlaylistDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;        

        public PlaylistDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;           
        }

        public async Task<bool> DeletePlaylistDetail(Guid PlaylistId, Guid MediaId)
        {
            var rs = _unitOfWork.Repository<PlaylistDetail>().Find(x => x.PlaylistId == PlaylistId & x.MediaId == MediaId);
            if(rs == null)
            {
                return false;
            }
            _unitOfWork.Repository<PlaylistDetail>().HardDelete(rs.Id);
            try
            {
                _unitOfWork.Commit();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
            

        }

        public async Task<List<PlaylistDetail>> GetDetailsPlaylistByID(Guid PlaylistId)
        {
            List<PlaylistDetail> list = new List<PlaylistDetail>();
            var query=_unitOfWork.Repository<PlaylistDetail>().FindAll(x => x.PlaylistId == PlaylistId);
            foreach(var item in query)
            {
                list.Add(new Data.Entity.PlaylistDetail()
                {
                    Id = item.Id,
                    MediaId= item.MediaId,
                    PlaylistId = item.PlaylistId,
                    NumericalOrder=item.NumericalOrder
                });
            }
            return list;
        }

        public async Task<bool> PostPlaylistDetail(Guid PlaylistId, Guid MediaId)
        {
            int order = 1;
            var playlistDetails = _unitOfWork.Repository<Data.Entity.PlaylistDetail>().FindAll(x => x.PlaylistId == PlaylistId)
                .OrderByDescending(p => p.NumericalOrder).FirstOrDefault();
            if (playlistDetails != null)
            {
                order = Int32.Parse((playlistDetails.NumericalOrder + 1).ToString());
            }
            try
            {
                _unitOfWork.Repository<PlaylistDetail>().Insert(new PlaylistDetail()
                {
                    Id = Guid.NewGuid(),
                    PlaylistId = PlaylistId,
                    MediaId = MediaId,
                    NumericalOrder = order
                });
                _unitOfWork.Commit();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
            
        }
    }
}
