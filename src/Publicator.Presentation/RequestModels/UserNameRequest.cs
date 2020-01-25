using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class UsernameRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
