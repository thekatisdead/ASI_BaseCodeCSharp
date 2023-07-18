using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class PublicApplicationFormRepository : BaseRepository, IPublicApplicationFormRepository
    {
        private readonly BasecodeContext _context;
        public PublicApplicationFormRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public void AddForm(PublicApplicationForm applicationForm)
        {
            _context.PublicApplicationForm.Add(applicationForm);
            _context.SaveChanges();
        }
        public PublicApplicationForm GetById(int id)
        {
            return _context.PublicApplicationForm.Find(id);
        }
    }
}
