using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class CurrentMediaInStoresDTO
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public Guid StoreId { get; set; }
        public Guid PlaylistId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public TimeSpan? TimeToPlay { get; set; }
    }
}
