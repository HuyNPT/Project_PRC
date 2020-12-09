using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.WebAdminAS.Models.Media
{
    public class CategoryMedia
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Category> Category { get; set; }
    }
}
