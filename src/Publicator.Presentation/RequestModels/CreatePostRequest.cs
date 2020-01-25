using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CreatePostRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public Guid? CommunityId { get; set; }
        [Required]
        public List<string> Tags { get; set; }
    }
}
