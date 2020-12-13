using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.RequestAdmin
{
    public class GetPlaylistAdminRequest
    {
        public enum _SortType1 { NoSort = 0, SortAsce = 1, SortDesc = 2 }
        public _SortType1 SortType { get; set; }
        public bool IsPaging { get; set; }
        public int PageNumber { get; set; }
        public int PageLimitItem { get; set; }
        public string SearchKey { get; set; }
    }
}
