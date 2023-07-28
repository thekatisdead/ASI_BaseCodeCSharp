using AutoMapper;
using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class JobOpeningService : IJobOpeningService
    {
        private readonly IJobOpeningRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public JobOpeningService(IJobOpeningRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<JobOpeningViewModel> RetrieveAll()
        {
            try
            {
                var data = _repository.RetrieveAll().Select(s => new JobOpeningViewModel
                {
                    Id = s.Id,
                    Position = s.Position,
                    JobType = s.JobType,
                    Salary = s.Salary,
                    Hours = s.Hours,
                    Shift = s.Shift,
                    Description = s.Description
                }).ToList();

                // Log the number of job openings retrieved
                _logger.Info("Retrieved {jobOpeningCount} job openings.", data.Count);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving all job openings: {errorMessage}", ex.Message);
                throw;
            }
        }

        public JobOpeningViewModel GetById(int id)
        {
            try
            {
                var data = _repository.GetById(id);
                _logger.Info("Retrieving job opening by ID: {applicantId}", id);
                return _mapper.Map<JobOpeningViewModel>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving job opening with ID {jobOpeningId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public void Add(JobOpening jobOpening)
        {
            try
            {
                jobOpening.CreatedBy = System.Environment.UserName;
                jobOpening.CreatedTime = DateTime.Now;
                jobOpening.UpdatedBy = System.Environment.UserName;
                jobOpening.UpdatedTime = DateTime.Now;
                _repository.Add(jobOpening);

                _logger.Info("Added a new job opening with ID {jobOpeningId}.", jobOpening.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new job opening: {errorMessage}", ex.Message);
                throw;
            }
        }

        public void Update(JobOpening jobOpening)
        {
            try
            {
                var job = _repository.GetById(jobOpening.Id);
                job.Position = jobOpening.Position;
                job.JobType = jobOpening.JobType;
                job.Salary = jobOpening.Salary;
                job.Hours = jobOpening.Hours;
                job.Shift = jobOpening.Shift;
                job.Description = jobOpening.Description;
                job.UpdatedBy = System.Environment.UserName;
                job.UpdatedTime = DateTime.Now;

                _repository.Update(job);

                _logger.Info("Updated job opening with ID {jobOpeningId}.", jobOpening.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating job opening with ID {jobOpeningId}: {errorMessage}", jobOpening.Id, ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var job = _repository.GetById(id);
                _repository.Delete(job);

                _logger.Info("Deleted job opening with ID {jobOpeningId}.", id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting job opening with ID {jobOpeningId}: {errorMessage}", id, ex.Message);
                throw;
            }
        }

        public JobOpeningViewModel GetMostRecentJobOpening()
        {
            try
            {
                // Fetch the most recent job opening from the repository and map it to JobOpeningViewModel
                var recentJobOpening = _repository.GetMostRecentJobOpening();
                return _mapper.Map<JobOpeningViewModel>(recentJobOpening);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting the most recent job opening: {errorMessage}", ex.Message);
                throw;
            }
        }
    }
}