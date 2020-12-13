using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Models
{
    public class PlaylistResponse
    {
        public Guid Id { get; set; }
        [Required]
        public string PlaylistName { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public int DateFillter { get; set; }
        public int TimePlayed { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public Guid? BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<CategoryPlaylist> CategoryPlaylists { get; set; }

    }
}
