using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class ScheduleViewModel
    {

        public int? ScheduleId { get; set; }
        public int? InterviewerId { get; set; }
        public int? JobId { get; set; }
        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.Time,ErrorMessage ="Invalid Time")]
        [Display(Name = "Time")]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "End time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Invalid Time")]
        [Display(Name = "Time")]
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        public string Instruction { get; set; }
    }
}
