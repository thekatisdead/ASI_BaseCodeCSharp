using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Interfaces
{
    public interface IPublicApplicationFormService
    {
        void AddForm(PublicApplicationFormViewModel applicationForm);
        public PublicApplicationFormViewModel GetById(int id);
        public PublicApplicationFormViewModel GetByApplicationId(int id);
        public int CountResponded(int id);
    }
}
