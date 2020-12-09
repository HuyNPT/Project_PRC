using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Api.Models.Request
{
    public class PutCurrentMediaRequest
    {
        public Guid MediaId { get; set; }
        public Guid StoreId { get; set; }
        public Guid PlaylistId { get; set; }
        public String TimeStart { get; set; }
        public String  TimeEnd { get; set; }
        public TimeSpan? TimeToPlay { get; set; }
    }
}
