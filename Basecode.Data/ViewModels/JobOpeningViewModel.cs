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

        [Required]
        [StringLength(20)]
        [Display(Name = "Position")]
        public string Position { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Display(Name = "Job Type")]
        public string JobType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Salary")]
        [Range(0, 999999.99)]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Hours")]
        [RegularExpression(@"^(?:[8-9]|1[0-9]|2[0-4])$", ErrorMessage = "Work Hours must be between 8 and 24.")]
        public int Hours { get; set; }

        [Required]
        [Display(Name = "Shift")]
        [StringLength(10)]
        [RegularExpression("^(Morning|Night)$", ErrorMessage = "Invalid shift. Please enter 'Morning' or 'Night'.")]
        public string Shift { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
