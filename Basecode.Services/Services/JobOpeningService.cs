using AutoMapper;
using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class JobOpeningService : IJobOpeningService
    {
        /// <summary>
        /// Creates an instance of IJobOpeningRepository
        /// Creates an instance of AutoMapper
        /// </summary>
        private readonly IJobOpeningRepository _repository;
        private readonly IMapper _mapper;
        public JobOpeningService(IJobOpeningRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve all Job Openings from the database
        /// </summary>
        /// <returns>A list of JobOpeningViewModel object representing all Job Openings.</returns>
        public List<JobOpeningViewModel> RetrieveAll()
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
            return data;
        }

        /// <summary>
        /// Retrieves a Job Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a JobOpeningViewModel object representing a Job Opening</returns>
        public JobOpeningViewModel GetById(int id)
        {
            var data = _repository.GetById(id);
            return _mapper.Map<JobOpeningViewModel>(data);
        }

        /// <summary>
        /// Create/Add a new Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Add(JobOpening jobOpening)
        {
            jobOpening.CreatedBy = System.Environment.UserName;
            jobOpening.CreatedTime = DateTime.Now;
            jobOpening.UpdatedBy = System.Environment.UserName;
            jobOpening.UpdatedTime = DateTime.Now;
            _repository.Add(jobOpening);
        }

        /// <summary>
        /// Updates a selected Job Opening
        /// </summary>
        /// <param name="jobOpening"></param>
        public void Update(JobOpening jobOpening) 
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
        }
		/// <summary>
		/// Deletes a selected Job Opening
		/// </summary>
		/// <param name="jobOpening"></param>
		public void Delete(int id)
        {
            var job = _repository.GetById(id);
            _repository.Delete(job);
        }
    }
}
