using System.ComponentModel.DataAnnotations;

namespace Basecode.WebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
