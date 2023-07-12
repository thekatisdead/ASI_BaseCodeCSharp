using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IPublicApplicationFormRepository
    {
        /// <summary>
        /// Add a new public application form into the public application system using the provided data.
        /// </summary>
        /// <param name="applicationForm">An instance of the PublicApplicationFormViewModel class that holds the required data.</param>
        void AddForm(PublicApplicationForm applicationForm);
    }
}
