using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class PlaylistInStoreDTO
    {
        public Guid Id { get; set; }
        public int Priotity { get; set; }
        public int NumberOfPlays { get; set; }      
        public Guid StoreId { get; set; }
        public Guid PlaylistId { get; set; }
        public double OrderPlay { get; set; }
        public string PlaylistName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<CategoryPlaylist> CategoryPlaylists { get; set; }

    }
}
