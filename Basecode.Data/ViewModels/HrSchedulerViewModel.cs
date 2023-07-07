using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class HrSchedulerViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(15, ErrorMessage = "First name must not exceed 15 characters.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(15, ErrorMessage = "Last name must not exceed 15 characters.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [DataType(DataType.Time)]
        [Display(Name = "End time")]
        [Compare("StartTime", ErrorMessage = "Start time must be earlier than End time.")]
        public DateTime EndTime { get; set; }

        [StringLength(200, ErrorMessage = "Additional instruction must not exceed 200 characters.")]
        [Display(Name = "Additional instruction")]
        public string? AdditionalInstruction { get; set; }
    }
}
