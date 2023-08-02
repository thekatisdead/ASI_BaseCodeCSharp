using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    /// <summary>
    /// Job Opening model shown on the razor page
    /// </summary>
    public class JobOpeningViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [StringLength(20, ErrorMessage = "Position must not exceed 20 characters.")]
        [Display(Name = "Position")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job type is required.")]
        [StringLength(20, ErrorMessage = "Job type must not exceed 20 characters.")]
        [Display(Name = "Job Type")]
        public string JobType { get; set; } = string.Empty;

        public string? HREmail { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 999999.99, ErrorMessage = "Salary must not exceed 999,999.99.")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Working hours is required.")]
        [RegularExpression(@"^(?:[8-9]|1[0-9]|2[0-4])$", ErrorMessage = "Work Hours must be between 8 and 24.")]
        [Display(Name = "Hours")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Shift is required.")]
        [StringLength(10, ErrorMessage = "Shift must not exceed 10 characters.")]
        [RegularExpression("^(Morning|Night)$", ErrorMessage = "Invalid shift. Please enter 'Morning' or 'Night'.")]
        [Display(Name = "Shift")]
        public string Shift { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Description must not exceed 200 characters.")]
        [Display(Name = "Description(Optional)")]
        public string? Description { get; set; }
    }
}
