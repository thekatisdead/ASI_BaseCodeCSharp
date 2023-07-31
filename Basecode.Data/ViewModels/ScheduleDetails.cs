using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class ScheduleDetails
    {
        public int ScheduleId;
        public int JobId;
        public int InterviewerId;
        public int ApplicantId;
        public string Position;
        public string FirstName;
        public string LastName;
        public string StartTime;
        public string EndTime;
        public string Date;
        public string instruction;
        public string TypeOfExam;
        public List<ApplicantListViewModel> Applicants { get; set; }
    }
}
