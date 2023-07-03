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
        private readonly IJobOpeningRepository _repository;
        private readonly IMapper _mapper;
        public JobOpeningService(IJobOpeningRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

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

        public JobOpeningViewModel GetById(int id)
        {
            var data = _repository.GetById(id);
            return _mapper.Map<JobOpeningViewModel>(data);
        }

        public void Add(JobOpening jobOpening)
        {
            jobOpening.CreatedBy = System.Environment.UserName;
            jobOpening.CreatedTime = DateTime.Now;
            jobOpening.UpdatedBy = System.Environment.UserName;
            jobOpening.UpdatedTime = DateTime.Now;
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
