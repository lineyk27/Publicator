using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class UserPostsRequest : PageRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
