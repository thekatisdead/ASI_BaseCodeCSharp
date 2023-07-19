using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class InterviewerViewModel
    {
        public int? InterviewerId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(15, ErrorMessage = "First name must not exceed 15 characters.")]
        [Display(Name = "First name")]
        public string? FistName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(15, ErrorMessage = "Last name must not exceed 15 characters.")]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? ContactNo { get; set; }
    }
}
