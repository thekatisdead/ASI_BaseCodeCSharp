using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public  class ApplicantsScheduleRepo:  BaseRepository, IApplicantsScheduleRepo
    {
        private readonly BasecodeContext _context;
        public ApplicantsScheduleRepo(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public void Add(ApplicantsSchedule schedule)
        {
            _context.ApplicantsSchedule.Add(schedule);
            _context.SaveChanges();
        }
        public IQueryable<ApplicantsSchedule> GetAll() 
        {
            return this.GetDbSet<ApplicantsSchedule>();
        }
    }
}
