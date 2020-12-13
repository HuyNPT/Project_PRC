using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class Media
    {
        public Guid Id { get; set; }
        public string? MusicName { get; set; }
        public Guid? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsDelete { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? Author { get; set; }
        public string? Singer { get; set; }
        public int? Type { get; set; }

        public string FileName { get; set; }

        public ICollection<PlaylistDetails> PlaylistDetails { get; set; }
        public ICollection<CategoryMedia> CategoryMedia { get; set; }
        

    }
}
