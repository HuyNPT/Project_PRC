using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class PlaylistDetails
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid MusicId { get; set; }
        public int? NumericalOrder { get; set; }
    }
}
