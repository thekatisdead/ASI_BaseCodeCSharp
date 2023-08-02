using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IScheduleService
    {
        public void Add(Schedule schedule);
        public List<ScheduleViewModel> GetAll();
        public List<ScheduleDetails> GetDetails();
        public ScheduleViewModel GetById(int id);
        public void UpdateSchedule(Schedule schedule);
        public void DeleteSchedule(int id);
        public List<object> GetApplicantListAccordingToJobApplied(int jobId);
        public List<object> GetJobs();
        public List<object> GetInterviewersServ();
        public void AddApplicantSchedule(ApplicantsSchedule schedule);
        public int GetMostRecentSchedId();
        public List<ApplicantInfo> GetApplicantListAccordingToSchedule(int sched);
    }
}
