﻿using Basecode.Data.Models;
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

        /// <summary>
        /// Retrieves a PublicApplicationForm from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the PublicApplicationForm to retrieve.</param>
        /// <returns>The PublicApplicationForm entity with the specified ID if found; otherwise, returns null.</returns>
        public PublicApplicationForm GetById(int id);

        /// <summary>
        /// Retrieves a PublicApplicationForm from the database by its ApplicationID.
        /// </summary>
        /// <param name="id">The ApplicationID of the PublicApplicationForm to retrieve.</param>
        /// <returns>The PublicApplicationForm entity with the specified ApplicationID if found; otherwise, returns null.</returns>
        public PublicApplicationForm GetByApplicationId(int id);
        public void Responded(int id, int trigger);
        public int CountResponded(int id);
    }
}
