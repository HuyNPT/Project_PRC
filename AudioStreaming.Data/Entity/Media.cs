using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class Media
    {
        public Media()
        {
            CategoryMedia = new HashSet<CategoryMedia>();
            CurrentMediaInStore = new HashSet<CurrentMediaInStore>();
            PlaylistDetail = new HashSet<PlaylistDetail>();
        }

        public Guid Id { get; set; }
        public string MusicName { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Singer { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public Guid? OwnerCode { get; set; }

        public virtual ICollection<CategoryMedia> CategoryMedia { get; set; }
        public virtual ICollection<CurrentMediaInStore> CurrentMediaInStore { get; set; }
        public virtual ICollection<PlaylistDetail> PlaylistDetail { get; set; }
    }
}
