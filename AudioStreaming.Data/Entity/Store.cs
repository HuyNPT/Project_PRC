using System;
using System.Collections.Generic;

namespace ASS_PRC.Data.Entity
{
    public partial class Store
    {
        public Store()
        {
            CurrentMediaInStore = new HashSet<CurrentMediaInStore>();
            PlaylistInStore = new HashSet<PlaylistInStore>();
            TimeSubmit = new HashSet<TimeSubmit>();
        }

        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Wifi { get; set; }
        public string PassWifi { get; set; }
        public Guid BrandId { get; set; }
        public bool IsDelete { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<CurrentMediaInStore> CurrentMediaInStore { get; set; }
        public virtual ICollection<PlaylistInStore> PlaylistInStore { get; set; }
        public virtual ICollection<TimeSubmit> TimeSubmit { get; set; }
    }
}
