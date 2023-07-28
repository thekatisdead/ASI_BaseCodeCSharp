using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class HrSchedulerService : IHrSchedulerService
    {
        private readonly IHrSchedulerRepository _repository;

        public HrSchedulerService(IHrSchedulerRepository repository)
        {
            _repository = repository;
        }
    }
}
