using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Models.Media
{
    public class CreateMedia
    {
        [Required]
        public string MediaName { get; set; }
        [Required]
        public string ImgURL { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Singer { get; set; }
        public List<string> TypeMedia { get; set; }
        public List<Category> Category { get; set; }

        public string PlaylistID { get; set; }
        public string PlaylistName { get; set; }
        public string FileName { get; set; }

        public string Url { get; set; }
    }
}
