﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AudioStreaming.Services.DTO
{
    public class Playlist
    {
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

        public virtual ICollection<CategoryPlaylist> CategoryPlaylist { get; set; }

    }
   
}
