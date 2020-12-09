using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Models.PlaylistDetail
{
    public class PlaylistDetailcs
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid MediaId { get; set; }
        public int NumericalOrder { get; set; }
    }
}
