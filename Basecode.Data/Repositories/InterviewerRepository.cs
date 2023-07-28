using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class InterviewerRepository : BaseRepository, IInterviewerRepository
    {
        private readonly BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public InterviewerRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public void Add(Interviewer interviewer)
        {
            try
            {
                _context.Interviewer.Add(interviewer);
                _context.SaveChanges();
                _logger.Info("Interviewer added successfully. ID: {interviewerId}", interviewer.InterviewerId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding Interviewer: {errorMessage}", ex.Message);
                throw;
            }
        }

        public IQueryable<Interviewer> GetAll()
        {
            _logger.Info("Retrieving all Interviewers from the database.");
            return this.GetDbSet<Interviewer>();
        }

        public void Update(Interviewer interviewer)
        {
            try
            {
                _context.Interviewer.Update(interviewer);
                _context.SaveChanges();
                _logger.Info("Interviewer with ID {interviewerId} updated successfully.", interviewer.InterviewerId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating Interviewer with ID {interviewerId}: {errorMessage}", interviewer.InterviewerId, ex.Message);
                throw;
            }
        }

        public Interviewer GetById(int id)
        {
            _logger.Info("Retrieving Interviewer by ID: {interviewerId}", id);
            return _context.Interviewer.Find(id);
        }

        public void Delete(Interviewer interviewer)
        {
            try
            {
                _context.Interviewer.Remove(interviewer);
                _context.SaveChanges();
                _logger.Info("Interviewer with ID {interviewerId} deleted successfully.", interviewer.InterviewerId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting Interviewer with ID {interviewerId}: {errorMessage}", interviewer.InterviewerId, ex.Message);
                throw;
            }
        }
    }
}