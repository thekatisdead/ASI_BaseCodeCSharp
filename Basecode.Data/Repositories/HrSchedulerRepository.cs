using Basecode.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class HrSchedulerRepository : BaseRepository, IHrSchedulerRepository
    {
        public readonly BasecodeContext _context;

        public HrSchedulerRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
    }
}
