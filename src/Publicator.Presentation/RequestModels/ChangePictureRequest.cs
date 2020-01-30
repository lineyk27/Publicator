using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class ChangePictureRequest
    {
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }
    }
}
