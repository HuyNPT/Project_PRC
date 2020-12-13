using System.ComponentModel.DataAnnotations;

namespace ASS_PRC.Api.Models.Request
{
    public class  AuthenticateModel
    {
        [Required]
        public string IdToken { get; set; }
        [Required]
        public string fcmToken { get; set; }

    }
}
