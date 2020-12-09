using System.ComponentModel.DataAnnotations;

namespace AudioStreaming.WebAdminAS.Services.Requests
{
    public class AuthenticateModelWebAdmin
    {
        [Required]
        public string IdToken { get; set; }
        
    }
}
