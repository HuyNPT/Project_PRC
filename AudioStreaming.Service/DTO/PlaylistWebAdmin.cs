using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class PlaylistWebAdmin
    {
        public Guid Id { get; set; }
        public string PlaylistName { get; set; }       
        public DateTime ModifyDate { get; set; }       
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public int DateFillter { get; set; }
        public int TimePlayed { get; set; }
        public string ImageUrl { get; set; }
        public Guid? BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<CategoryPlaylist> CategoryPlaylists { get; set; }
    }
}
