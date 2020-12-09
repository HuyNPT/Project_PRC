using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Models.Media
{
    public class MediaDTO
    {
        public int Counter { get; set; }
        public List<MediaModel> ListMedia { get; set; }
    }
}
