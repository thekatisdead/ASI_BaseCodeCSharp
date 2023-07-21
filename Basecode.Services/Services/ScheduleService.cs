using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.ViewModels;
using System.Data;

namespace Basecode.Services.Services
{
    public class ScheduleService:IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IJobOpeningRepository  _jobOpeningRepository;
        private readonly IInterviewerRepository _interviewerRepository;
        public  ScheduleService(IScheduleRepository scheduleRepository, IJobOpeningRepository jobOpeningRepository,IInterviewerRepository interviewerRepository)
        {
            _scheduleRepository = scheduleRepository;
            _jobOpeningRepository = jobOpeningRepository;
            _interviewerRepository = interviewerRepository;
        }
        public void Add(Schedule schedule)
        {
            schedule.CreatedBy = "Jimwill";
            schedule.CreatedTime= DateTime.Now;
            schedule.UpdatedBy = "Jimwill";
            schedule.UpdatedTime = DateTime.Now;
            _scheduleRepository.Add(schedule);
        }
        public List<ScheduleViewModel> GetAll() 
        {
            var schedules = _scheduleRepository.GetAll().Select(s => new ScheduleViewModel { 
                ScheduleId= s.ScheduleId,
                InterviewerId = s.InterviewerId,
                JobId= s.JobId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Instruction = s.Instruction
            }).ToList();

            return schedules;
        }
        public List<ScheduleDetails> GetDetails()
        {
            var interviewers = _interviewerRepository.GetAll().Select(s => new
            {
                InterviewerId = s.InterviewerId,
                Firstname = s.FirstName,
                LastName = s.LastName
            });
            var jobopnings = _jobOpeningRepository.RetrieveAll().Select(s => new
            {
                JobId= s.Id,
                Position =s.Position
            });
            var schedule = _scheduleRepository.GetAll().Select(s => new
            {
                ScheduleId = s.ScheduleId,
                InterviewerId = s.InterviewerId,
                JobId = s.JobId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Date = s.Date,
                Instruction = s.Instruction
            });
            var details = from sched in schedule join inter in interviewers on sched.InterviewerId equals inter.InterviewerId join job in jobopnings on sched.JobId equals job.JobId
                          select new ScheduleDetails { 
                                 Position =job.Position,
                                 FirstName = inter.Firstname,
                                 LastName= inter.LastName,
                                 StartTime=sched.StartTime,
                                 EndTime = sched.EndTime,
                                 Date = sched.Date,
                                 instruction = sched.Instruction                       
                          };
            return details.ToList();                 
        }
    }
}
