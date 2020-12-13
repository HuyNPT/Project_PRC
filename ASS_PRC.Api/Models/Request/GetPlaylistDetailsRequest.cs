using System;

namespace ASS_PRC.Api.Models.Request
{
    public class GetPlaylistDetailsRequest
    {
        public enum _SortType { NoSort = 0, SortAsce = 1, SortDesc = 2 }
        public _SortType SortType { get; set; }
        public bool IsPaging { get; set; }
        public int PageNumber { get; set; }
        public int PageLimitItem { get; set; }
        public enum _Type { Music = 1 , Advertising = 2 }
        public _Type Type { get; set; }       
    }
}
