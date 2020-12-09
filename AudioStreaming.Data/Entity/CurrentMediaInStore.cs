using System;
using System.Collections.Generic;

namespace AudioStreaming.Data.Entity
{
    public partial class CurrentMediaInStore
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public Guid StoreId { get; set; }
        public Guid PlaylistId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public TimeSpan? TimeToPlay { get; set; }

        public virtual Media Media { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual Store Store { get; set; }
    }
}
