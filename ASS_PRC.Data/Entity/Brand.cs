using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class Brand
    {
        public Brand()
        {
            Playlist = new HashSet<Playlist>();
        }

        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
