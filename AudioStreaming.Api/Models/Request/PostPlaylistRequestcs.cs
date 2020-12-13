using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.Request
{
    public class PostPlaylistRequestcs
    {      
        public string PlaylistName { get; set; }                
        public int DateFillter { get; set; }
        public string ImageUrl { get; set; }        
        public List<Guid> Category { get; set; }
    }
}
