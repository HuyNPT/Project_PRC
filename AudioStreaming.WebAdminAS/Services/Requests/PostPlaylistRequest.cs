using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services.Requests
{
    public class PostPlaylistRequest
    {
        public string playlistName { get; set; }
        public int dateFillter { get; set; }
        public string imageUrl { get; set; }
        public List<Guid> category { get; set; }
    }
}
