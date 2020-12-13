using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.Responses
{
    public class UserFavoritePlaylistsResponse
    {
        public IList<UserFavoritePlaylist> userFavoritePlaylists;
    }
    public class UserFavoritePlaylist
    {
        public Guid Id { get; set; }
        public string? PlaylistName { get; set; }
        public Guid? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsDelete { get; set; }
        public int? DateFillter { get; set; }
        public Guid? BrandId { get; set; }
        public int? TimePlayed { get; set; }
        public string? ImageUrl { get; set; }
        public string? BrandName { get; set; }
    }
}
