using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class ApplicantsSchedule
    {
        public int Id { get; set; } 
        public int ScheduleId { get; set; }
        public int ApplicantId { get; set; }        
    }
}
