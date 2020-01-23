using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Publicator.Presentation.RequestModels
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(6)]
        // TODO add regexp for check is valid username
        [RegularExpression("",ErrorMessage ="Wrong username format")]
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
