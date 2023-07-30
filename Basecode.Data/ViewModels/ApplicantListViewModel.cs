using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class ApplicantListViewModel
    {
        public int Id { get; set; }
        public int? FormID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? EmailAddress { get; set; }
        public string? Tracker { get; set; }
        public string? Grading { get; set; }
        public int JobApplied { get; set; }
    }
}
