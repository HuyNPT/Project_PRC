using System;
using System.Collections.Generic;

namespace AudioStreaming.Data.Entity
{
    public partial class TimeSubmit
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? TimeSubmit1 { get; set; }
        public Guid? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual Account User { get; set; }
    }
}
