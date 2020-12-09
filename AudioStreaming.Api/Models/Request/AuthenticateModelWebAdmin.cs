using System.ComponentModel.DataAnnotations;

namespace AudioStreaming.Api.Models.Request
{
    public class AuthenticateModelWebAdmin
    {
        [Required]
        public string IdToken { get; set; }
        
    }
}
