using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.ViewModels;
namespace Basecode.Services.Services
{
    public class ScheduleService:IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        public  ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
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
    }
}
