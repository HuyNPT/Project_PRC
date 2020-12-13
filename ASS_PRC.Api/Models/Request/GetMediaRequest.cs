using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.Request
{
    public class GetMediaRequest
    {
        public enum _SortType2 { NoSort = 0, SortAsce = 1, SortDesc = 2 }
        public _SortType2 SortType { get; set; }
        public bool IsPaging { get; set; }
        public int PageNumber { get; set; }
        public int PageLimitItem { get; set; }
        public enum _Type2 { Music = 1, Advertising = 2 }
        public _Type2 Type { get; set; }
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }

        public string CategoryName { get; set; }
    }
}
