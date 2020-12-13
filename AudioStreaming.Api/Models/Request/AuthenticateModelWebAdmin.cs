using System.ComponentModel.DataAnnotations;

namespace ASS_PRC.Api.Models.Request
{
    public class AuthenticateModelWebAdmin
    {
        [Required]
        public string IdToken { get; set; }
        
    }
}
