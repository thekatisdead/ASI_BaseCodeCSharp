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
        public  List<JobOpeningViewModel> JobOpenings { get; set; }
        public List<InterviewerViewModel> Interviewers { get; set; }    
        public int? ScheduleId { get; set; }
        [Required(ErrorMessage = "Select an Interviewer")]
        public int InterviewerId { get; set; }
        [Required(ErrorMessage = "Select a Job Opening")]
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
