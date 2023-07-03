using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
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
        private readonly IJobOpeningRepository _repository;
        public JobOpeningService(IJobOpeningRepository repository)
        {
            _repository = repository;
        }

        public List<JobOpening> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public JobOpening GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(JobOpening jobOpening)
        {
            jobOpening.CreatedBy = System.Environment.UserName;
            jobOpening.CreatedTime = DateTime.Now;
            _repository.Add(jobOpening);
        }

        public void Update(JobOpening jobOpening) 
        {
            var job = _repository.GetById(jobOpening.Id);
            job.Position = jobOpening.Position;
            job.JobType = jobOpening.JobType;
            job.Salary = jobOpening.Salary;
            job.Hours = jobOpening.Hours;
            job.Description = jobOpening.Description;
            job.UpdatedBy = System.Environment.UserName;
            job.UpdatedTime = DateTime.Now;

            _repository.Update(job);
        }
    }
}
