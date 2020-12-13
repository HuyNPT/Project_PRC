using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class CategoryPlaylist
    {
        public Guid Id { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid CategoryId { get; set; }

        public ICollection<Category> Category { get; set; }
    }
}
