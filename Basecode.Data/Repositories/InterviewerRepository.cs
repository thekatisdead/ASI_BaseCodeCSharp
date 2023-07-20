using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class InterviewerRepository: BaseRepository, IInterviewerRepository
    {
        private readonly BasecodeContext _context;
        public InterviewerRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork) 
        {
            _context= context;
        }
        public void Add(Interviewer interviewer)
        {
            _context.Interviewer.Add(interviewer);
            _context.SaveChanges();
        }
        public IQueryable <Interviewer> GetAll()
        {
            return this.GetDbSet<Interviewer>();
        }
    }
}
