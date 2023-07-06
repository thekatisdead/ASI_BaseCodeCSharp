using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class SignUpViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string? EmailAddress { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact number must be 11 digits.")]
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = System.Environment.UserName;
    }
}
