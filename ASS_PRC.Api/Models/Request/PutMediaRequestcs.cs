using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS_PRC.Api.Models.Request
{
    public class PutMediaRequestcs
    {
        public Guid MediaID { get; set; }
        public string MediaName { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Author { get; set; }
        public string Singer { get; set; }
        public int Type { get; set; }        
        public List<Guid> Category { get; set; }
        public string FileName { get; set; }
    }
}
