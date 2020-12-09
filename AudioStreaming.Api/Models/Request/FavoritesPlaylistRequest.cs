using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Api.Models.Request
{
    public class FavoritesPlaylistRequest
    {
        public Guid PlaylistId { get; set; }
        public Guid AccountId { get; set; }
    }
}
