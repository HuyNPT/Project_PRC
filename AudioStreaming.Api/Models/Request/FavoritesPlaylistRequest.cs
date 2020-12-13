using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.Request
{
    public class FavoritesPlaylistRequest
    {
        public Guid PlaylistId { get; set; }
        public Guid AccountId { get; set; }
    }
}
