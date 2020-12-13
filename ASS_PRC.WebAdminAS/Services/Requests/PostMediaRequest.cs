using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services.Requests
{
    public class PostMediaRequest
    {
        public string mediaName { get; set; }
        public string url { get; set; }
        public string imgUrl { get; set; }
        public string author { get; set; }
        public string singer { get; set; }
        public int type { get; set; }
        public string playlistId { get; set; }
        public List<Guid> category { get; set; }
        public string fileName { get; set; }
    }
}
