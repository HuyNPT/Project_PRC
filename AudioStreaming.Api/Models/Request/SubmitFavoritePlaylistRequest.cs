using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.Request
{
    public class SubmitFavoritePlaylistRequest
    {
        public Guid UserID { get; set; }
        public List<Guid> List { get; set; }
        public bool IsVip { get; set; }
        public Guid StoreId { get; set; }
    }
}
