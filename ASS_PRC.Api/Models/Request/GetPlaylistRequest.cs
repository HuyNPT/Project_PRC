

namespace ASS_PRC.Api.Models.Request
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
