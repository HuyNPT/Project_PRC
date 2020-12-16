using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class PlaylistInStore
    {
        public Guid Id { get; set; }
        public int Priotity { get; set; }
        public int NumberOfPlays { get; set; }
        public DateTime Date { get; set; }
        public Guid StoreId { get; set; }
        public Guid PlaylistId { get; set; }
        public double OrderPlay { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}
