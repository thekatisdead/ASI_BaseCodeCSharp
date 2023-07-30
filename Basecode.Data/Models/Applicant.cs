using System.ComponentModel.DataAnnotations;

using Basecode.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public int FormID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? EmailAddress { get; set; }
        public int JobApplied { get; set; }
        public string? Tracker { get; set; }
        public string Grading { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set;}
        public string? UpdatedBy{ get; set; }
    }
}
