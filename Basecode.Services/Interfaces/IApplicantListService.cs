﻿using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantListService
    {
        /// <summary>
        /// Retrieves all applicants from the repository.
        /// </summary>
        /// <returns>A list of ApplicantListViewModel objects representing all applicants.</returns>
        List<ApplicantListViewModel> RetrieveAll();
        //void UpdateStatus(int applicantId, string status);

        void ProceedTo(int applicantId, string step);
        public void UpdateStatus(int applicantID, string status);
        public void UpdateGrade(int applicantID, string grade);
    }
}
