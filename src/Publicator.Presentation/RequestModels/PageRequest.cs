using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class PageRequest
    {
        [Required]
        public int Page { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
