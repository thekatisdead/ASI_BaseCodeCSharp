using Basecode.Data.Interfaces;
using Basecode.Data.Models;
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
    }
}
