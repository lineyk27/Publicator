using System;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class SearchRequest : PageRequest
    {
        [Required]
        public string Query { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinimumRating { get; set; }
        public string Community { get; set; }
    }
}
