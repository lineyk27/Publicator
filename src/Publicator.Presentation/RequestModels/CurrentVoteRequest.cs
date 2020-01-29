using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CurrentVoteRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
