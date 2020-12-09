
using AudioStreaming.WebAdminAS.Models.Media;
using System.Collections.Generic;

namespace AudioStreaming.WebAdminAS.Models.PlaylistModel
{
    public class Playlist
    {
        public PlaylistResponse PlaylistSelected { get; set; }
        public List<MediaModel> ListMedia { get; set; }
    }
}
