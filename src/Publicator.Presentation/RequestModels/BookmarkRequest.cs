using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class BookmarkRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
