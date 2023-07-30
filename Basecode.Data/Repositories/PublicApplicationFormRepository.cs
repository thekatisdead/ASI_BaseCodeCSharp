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
        public PublicApplicationForm GetByApplicationId(int id)
        {
            return _context.PublicApplicationForm.FirstOrDefault(form => form.ApplicationID == id);
        }
        public int CountResponded(int id)
        {
            var _application = this.GetById(id);
            var total = 0;
            if (_application.AnsweredOne != null)
            {
                total++;
            }
            if (_application.AnsweredTwo != null)
            {
                total++;
            }
            if (_application.AnsweredThree != null)
            {
                total++;
            }
            return total;
        }
    }
}
