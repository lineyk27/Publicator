using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CreateCommunityRequest
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
