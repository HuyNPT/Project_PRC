using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class Category
    {
        public Category()
        {
            CategoryMedia = new HashSet<CategoryMedia>();
            CategoryPlaylist = new HashSet<CategoryPlaylist>();
        }

        public Guid Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<CategoryMedia> CategoryMedia { get; set; }
        public virtual ICollection<CategoryPlaylist> CategoryPlaylist { get; set; }
    }
}
