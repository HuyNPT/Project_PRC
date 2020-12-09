using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Services.Requests
{
    public class GetPlaylistRequest
    {
        public bool IsSort { get; set; }
        public bool IsDescending { get; set; }
        public bool IsPaging { get; set; }
        public int PageNumber { get; set; }
        public int PageLimitItem { get; set; }
        public string OrderBy { get; set; }
        public string SearchKey { get; set; }
    }
}
