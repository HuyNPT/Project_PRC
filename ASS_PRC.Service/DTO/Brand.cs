using System;
using System.Collections.Generic;
using System.Text;

namespace ASS_PRC.Services.DTO
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }
        public  IList<DTO.Playlist> Playlist { get; set; }

    }
}
