using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class PlaylistDetail
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid MediaId { get; set; }
        public int? NumericalOrder { get; set; }

        public virtual Media Media { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
