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
using AutoMapper;
using NLog;

namespace Basecode.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IJobOpeningRepository _jobOpeningRepository;
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IApplicantListRepository _applicantListRepository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ScheduleService(IScheduleRepository scheduleRepository, IJobOpeningRepository jobOpeningRepository, IInterviewerRepository interviewerRepository, IApplicantListRepository applicantListRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _jobOpeningRepository = jobOpeningRepository;
            _interviewerRepository = interviewerRepository;
            _applicantListRepository = applicantListRepository;
            _mapper = mapper;
        }

        public void Add(Schedule schedule)
        {
            try
            {
                schedule.CreatedBy = System.Environment.UserName;
                schedule.CreatedTime = DateTime.Now;
                schedule.UpdatedBy = System.Environment.UserName;
                schedule.UpdatedTime = DateTime.Now;
                _scheduleRepository.Add(schedule);
                _logger.Info("Schedule with ID {scheduleId} added successfully.", schedule.ScheduleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new schedule: {errorMessage}", ex.Message);
                throw;
            }
        }

        public List<ScheduleViewModel> GetAll()
        {
            try
            {
                var schedules = _scheduleRepository.GetAll().Select(s => new ScheduleViewModel
                {
                    ScheduleId = s.ScheduleId,
                    InterviewerId = s.InterviewerId,
                    JobId = s.JobId,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Instruction = s.Instruction
                }).ToList();

                return schedules;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving all schedules: {errorMessage}", ex.Message);
                throw;
            }
        }

        public List<ScheduleDetails> GetDetails()
        {
            try
            {
                var interviewers = _interviewerRepository.GetAll().Select(s => new
                {
                    InterviewerId = s.InterviewerId,
                    Firstname = s.FirstName,
                    LastName = s.LastName
                });
                var jobOpenings = _jobOpeningRepository.RetrieveAll().Select(s => new
                {
                    JobId = s.Id,
                    Position = s.Position
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
                var details = from sched in schedule
                              join inter in interviewers on sched.InterviewerId equals inter.InterviewerId
                              join job in jobOpenings on sched.JobId equals job.JobId
                              select new ScheduleDetails
                              {
                                  ScheduleId = sched.ScheduleId,
                                  JobId = job.JobId,
                                  InterviewerId = inter.InterviewerId,
                                  Position = job.Position,
                                  FirstName = inter.Firstname,
                                  LastName = inter.LastName,
                                  StartTime = sched.StartTime,
                                  EndTime = sched.EndTime,
                                  Date = sched.Date,
                                  instruction = sched.Instruction
                              };
                return details.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving schedule details: {errorMessage}", ex.Message);
                throw;
            }
        }

        public ScheduleViewModel GetById(int id)
        {
            try
            {
                var data = _scheduleRepository.GetById(id);
                return _mapper.Map<ScheduleViewModel>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving schedule with ID {scheduleId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public void UpdateSchedule(Schedule schedule)
        {
            try
            {
                var _schedule = _scheduleRepository.GetById(schedule.ScheduleId);
                _schedule.InterviewerId = schedule.InterviewerId;
                _schedule.JobId = schedule.JobId;
                _schedule.StartTime = schedule.StartTime;
                _schedule.EndTime = schedule.EndTime;
                _schedule.Date = schedule.Date;
                _schedule.Instruction = schedule.Instruction;
                _schedule.UpdatedBy = System.Environment.UserName;
                _schedule.UpdatedTime = DateTime.Now;

                _scheduleRepository.UpdateSchedule(_schedule);
                _logger.Info("Schedule with ID {scheduleId} updated successfully.", schedule.ScheduleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating schedule with ID {scheduleId}: {errorMessage}", schedule.ScheduleId, ex.Message);
                throw;
            }
        }

        public void DeleteSchedule(int id)
        {
            try
            {
                var schedule = _scheduleRepository.GetById(id);
                _scheduleRepository.DeleteSchedule(schedule);
                _logger.Info("Schedule with ID {scheduleId} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting schedule with ID {scheduleId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public List<object> GetApplicantListAccordingToJobApplied(int jobId)
        {
            try
            {
                var data = _applicantListRepository.RetrieveAll().Where(s => s.JobApplied == jobId).Select(s => new
                {
                    name = s.Firstname + " "+s.Lastname,
                    email = s.EmailAddress
                });
                List<object> resultList = data.Cast<object>().ToList();
                return resultList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving applicant list according to job applied for job ID {jobId}: {errorMessage}", jobId, ex.Message);
                throw;
            }
        }
    }
}