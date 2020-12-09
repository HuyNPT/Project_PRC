using AudioStreaming.WebAdminAS.Models.PlaylistModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AudioStreaming.WebAdminAS.Models.Media
{
    public class MediaModel
    {
        public Guid Id { get; set; }
        [Required]
        public string MusicName { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Url { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Singer { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public ICollection<PlaylistDetails> PlaylistDetails { get; set; }
        public ICollection<CategoryMedia> CategoryMedia { get; set; }
    }
}
