using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CommentsRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
