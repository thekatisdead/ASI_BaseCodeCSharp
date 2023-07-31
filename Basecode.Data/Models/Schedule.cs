using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; } 
        public int InterviewerId { get; set; } 
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        public string Instruction { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = System.Environment.UserName;
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; } = System.Environment.UserName;
    }
}
