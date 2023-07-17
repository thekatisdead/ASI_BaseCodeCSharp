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
        private readonly IMapper _mapper;

        public CurrentHiresService(ICurrentHiresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddHire(int applicantId, int jobId)
        {
            _repository.AddHire(applicantId, jobId);
        }
    }
}
