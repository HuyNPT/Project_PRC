using System.ComponentModel.DataAnnotations;

namespace AudioStreaming.Api.Models.Request
{
    public class  AuthenticateModel
    {
        [Required]
        public string IdToken { get; set; }
        [Required]
        public string fcmToken { get; set; }

    }
}
