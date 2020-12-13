using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class CategoryMedia
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Media Media { get; set; }
    }
}
