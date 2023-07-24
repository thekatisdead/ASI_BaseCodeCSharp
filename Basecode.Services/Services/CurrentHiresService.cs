using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class CurrentHiresService : ICurrentHiresService
    {
        private readonly ICurrentHiresRepository _repository;

        public CurrentHiresService(ICurrentHiresRepository repository)
        {
            _repository = repository;
        }

        public void AddHire(int applicantId, int jobId)
        {
            _repository.AddHire(applicantId, jobId);
        }
    }
}
