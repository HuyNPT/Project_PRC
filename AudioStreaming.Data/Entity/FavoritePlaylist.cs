using System;
using System.Collections.Generic;

namespace AudioStreaming.Data.Entity
{
    public partial class FavoritePlaylist
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
