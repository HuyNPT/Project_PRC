using System;
using System.Collections.Generic;

namespace AudioStreaming.Data.Entity
{
    public partial class Playlist
    {
        public Playlist()
        {
            CategoryPlaylist = new HashSet<CategoryPlaylist>();
            CurrentMediaInStore = new HashSet<CurrentMediaInStore>();
            FavoritePlaylist = new HashSet<FavoritePlaylist>();
            PlaylistDetail = new HashSet<PlaylistDetail>();
            PlaylistInStore = new HashSet<PlaylistInStore>();
        }

        public Guid Id { get; set; }
        public string PlaylistName { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public int DateFillter { get; set; }
        public int TimePlayed { get; set; }
        public string ImageUrl { get; set; }
        public Guid? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<CategoryPlaylist> CategoryPlaylist { get; set; }
        public virtual ICollection<CurrentMediaInStore> CurrentMediaInStore { get; set; }
        public virtual ICollection<FavoritePlaylist> FavoritePlaylist { get; set; }
        public virtual ICollection<PlaylistDetail> PlaylistDetail { get; set; }
        public virtual ICollection<PlaylistInStore> PlaylistInStore { get; set; }
    }
}
