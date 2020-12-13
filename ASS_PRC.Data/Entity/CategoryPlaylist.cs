using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class CategoryPlaylist
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
