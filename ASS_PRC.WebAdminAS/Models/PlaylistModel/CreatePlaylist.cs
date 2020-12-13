using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AudioStreaming.WebAdminAS.Models.PlaylistModel
{
    public class CreatePlaylist
    {
        [Required]
        public string PlaylistName { get; set; }
        [Required]
        public int DateFilter { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        
        public List<Category> Category { get; set; }
    }
}
