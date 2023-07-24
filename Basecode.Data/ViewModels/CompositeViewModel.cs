using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class CompositeViewModel
    {
        public JobOpeningViewModel? JobOpeningData { get; set; }
        public ApplicantListViewModel? ApplicantsData { get; set; }
    }
}
