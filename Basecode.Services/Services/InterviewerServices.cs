using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Services.Interfaces;
namespace Basecode.Services.Services
{
    public class InterviewerServices: IInterviewerServices
    {
        private readonly IInterviewerRepository _interviewerRepository;

        public InterviewerServices(IInterviewerRepository interviewerRepository)
        {
            _interviewerRepository = interviewerRepository;
        }

        public void Add(Interviewer interviewer)
        {
            interviewer.CreatedBy = "jimwill";
            interviewer.UpdatedBy = "jimwill";
            interviewer.CreatedTime= DateTime.Now;
            interviewer.UpdatedTime= DateTime.Now;
            _interviewerRepository.Add(interviewer);
        }
        public List<InterviewerViewModel> GetAll() 
        {
            var interviewers = _interviewerRepository.GetAll().Select(s => new InterviewerViewModel
            {
                InterviewerId=s.InterviewerId,
                FirstName=s.FirstName,
                LastName=s.LastName,
                Email=s.Email,
                ContactNo=s.ContactNo,
            }).ToList();

            return interviewers;
        }
    }
}
