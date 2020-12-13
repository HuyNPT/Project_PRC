using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class Store
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Wifi { get; set; }
        public string PassWifi { get; set; }
        public string BrandName { get; set; }

        public Guid BrandId { get; set; }
        public bool IsDelete { get; set; }
        public string ImageUrl { get; set; }

    }
}
