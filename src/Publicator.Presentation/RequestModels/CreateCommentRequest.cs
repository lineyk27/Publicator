using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class CreateCommentRequest
    {
        [Required]
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
