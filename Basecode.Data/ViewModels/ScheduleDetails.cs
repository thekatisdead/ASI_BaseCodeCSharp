using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class ScheduleDetails
    {
        public int JobId;
        public int InterviewerId;
        public string Position;
        public string FirstName;
        public string LastName;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
        public DateTime Date;
        public string instruction;
      
    }
}
