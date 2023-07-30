using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class ApplicantListRepository: BaseRepository, IApplicantListRepository
    {
        public readonly BasecodeContext _context;

        public ApplicantListRepository(IUnitOfWork unitOfWork, BasecodeContext context): base(unitOfWork) 
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable of Applicant containing all applicants.</returns>
        public IQueryable<Applicant> RetrieveAll()
        {
            return this.GetDbSet<Applicant>();
        }

        public void Add(Applicant applicant)
        {
            _context.Applicant.Add(applicant);
            _context.SaveChanges();
        }

        public Applicant GetById(int id)
        {
            // warning here is that it is possible that the return below
            // will return a null value
            return _context.Applicant.FirstOrDefault(a => a.Id == id);
        }
        public void Update(Applicant applicant)
        {
            _context.Update(applicant);
            _context.SaveChanges();
        }

        public void UpdateStatus(int applicantId, string status)
        {
            var applicant = _context.Applicant.Find(applicantId);
            if (applicant != null)
            {
                applicant.Grading = status;
                _context.SaveChanges();
            }
        }

        public void ProceedTo(int applicantId, string step)
        {
            var applicant = _context.Applicant.Find(applicantId);
            if (applicant != null)
            {
                applicant.Tracker = step;
                applicant.Grading = "On Going";
                _context.SaveChanges();
            }
        }

        public ApplicantListViewModel GetMostRecentApplicant()
        {
            // Fetch the most recent applicant's info from the database
            var recentApplicant = _context.Applicant
                .OrderByDescending(j => j.CreatedTime)
                .FirstOrDefault();

            if (recentApplicant == null)
            {
                // If no job openings are available, return a specific ViewModel with a message
                return new ApplicantListViewModel
                {
                    Firstname = "N/A",
                    Lastname = "N/A",
                    Tracker = "N/A",
                    JobApplied = 0
                };
            }

            // Map the Applicant model to ApplicantListViewModel
            var recentApplicantViewModel = new ApplicantListViewModel
            {
                Id = recentApplicant.Id,
                Firstname = recentApplicant.Firstname,
                Lastname = recentApplicant.Lastname,
                Tracker = recentApplicant.Tracker,
                JobApplied = recentApplicant.JobApplied
            };

            return recentApplicantViewModel;
        }
    }
}
