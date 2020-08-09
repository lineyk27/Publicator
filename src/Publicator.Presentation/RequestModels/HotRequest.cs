using System.ComponentModel.DataAnnotations;
using Publicator.Core;

namespace Publicator.Presentation.RequestModels
{
    public class HotPostsRequest : PageRequest
    {
        [Required]
        public HotPeriod Period { get; set; }
    }
}
