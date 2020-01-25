using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class IdRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
