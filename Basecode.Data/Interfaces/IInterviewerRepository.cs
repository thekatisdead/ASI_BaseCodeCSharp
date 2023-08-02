using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IInterviewerRepository
    {
        /// <summary>
        /// Adds a new Interviewer to the database.
        /// </summary>
        /// <param name="interviewer">The Interviewer object representing the interviewer to be added.</param>
        public void Add(Interviewer interviewer);

        /// <summary>
        /// Retrieves all Interviewers from the database.
        /// </summary>
        /// <returns>An IQueryable collection of Interviewer entities representing all interviewers in the database.</returns>
        public IQueryable<Interviewer> GetAll();

        /// <summary>
        /// Updates an existing Interviewer in the database.
        /// </summary>
        /// <param name="interviewer">The updated Interviewer object.</param>
        public void Update(Interviewer interviewer);

        /// <summary>
        /// Retrieves an Interviewer from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the Interviewer to retrieve.</param>
        /// <returns>The Interviewer entity with the specified ID if found; otherwise, returns null.</returns>
        public Interviewer GetById(int id);

        /// <summary>
        /// Deletes an existing Interviewer from the database.
        /// </summary>
        /// <param name="interviewer">The Interviewer object to be deleted.</param>
        public void Delete(Interviewer interviewer);
    }
}
