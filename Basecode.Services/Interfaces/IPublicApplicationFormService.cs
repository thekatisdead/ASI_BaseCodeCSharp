using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Interfaces
{
    public interface IPublicApplicationFormService
    {
        void AddForm(PublicApplicationFormViewModel applicationForm);
        public PublicApplicationFormViewModel GetById(int id);
        /// <summary>
        /// This function combines three tables which are Applicant, JobOpening and PublicApplicationForm.
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobId"></param>
        /// <returns>An object reference containg the Applicant's Public Application details</returns>
        public ApplicantDetails GetApplicationFormById(int applicantId, int jobId);
    }
}
