using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class VoteRequest
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public bool Up { get; set; }
    }
}
