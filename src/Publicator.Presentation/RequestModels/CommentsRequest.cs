using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CommentsRequest : PageRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
