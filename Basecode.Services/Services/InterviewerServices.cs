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
using AutoMapper;
namespace Basecode.Services.Services
{
    public class InterviewerServices: IInterviewerServices
    {
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IMapper _mapper;
        public InterviewerServices(IInterviewerRepository interviewerRepository,IMapper mapper)
        {
            _interviewerRepository = interviewerRepository;
            _mapper = mapper;
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
        public void Update(Interviewer interviewer) 
        {
            var data = _interviewerRepository.GetById(interviewer.InterviewerId);
            data.FirstName = interviewer.FirstName;
            data.LastName = interviewer.LastName;
            data.Email = interviewer.Email;
            data.ContactNo = interviewer.ContactNo;
            data.UpdatedBy = System.Environment.UserName;
            data.UpdatedTime = DateTime.Now;

            _interviewerRepository.Update(data);
        }
        public InterviewerViewModel GetById(int id)
        {
            var data= _interviewerRepository.GetById(id);
            return _mapper.Map<InterviewerViewModel>(data);
        }
    }
}
