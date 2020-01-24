using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CommunityPostsRequest : PageRequest
    {
        [Required]
        public Guid CommunityId { get; set; }
    }
}
