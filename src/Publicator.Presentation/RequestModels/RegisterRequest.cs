using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(6)]
        [RegularExpression(@"[a-zA-Z0-9\-.]{1,32}", ErrorMessage ="Wrong username format")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Wrong password format")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Wrong confirm password format")]
        public string ConfirmPassword { get; set; }
    }
}
