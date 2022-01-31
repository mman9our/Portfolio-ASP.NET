using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ULogin
    {


        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

    }
}

