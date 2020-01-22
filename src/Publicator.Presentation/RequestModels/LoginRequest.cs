using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class LoginRequest
    {
        [Required]
        [MinLength(6)]
        public string Login { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
