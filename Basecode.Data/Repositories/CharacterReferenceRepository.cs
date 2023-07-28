using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Basecode.Data.Repositories
{
    public class CharacterReferenceRepository : BaseRepository, ICharacterReferenceRepository
    {
        private readonly BasecodeContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferenceRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all character references.
        /// </summary>
        /// <returns>An IQueryable of CharacterReference.</returns>
        public IQueryable<CharacterReference> RetrieveAll()
        {
            try
            {
                _logger.Info("Retrieving all character references from the database.");
                return this.GetDbSet<CharacterReference>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while retrieving all character references: {errorMessage}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="characterReference">The character reference to add.</param>
        public void Add(CharacterReference characterReference)
        {
            try
            {
                _context.CharacterReference.Add(characterReference);
                _context.SaveChanges();
                _logger.Info("Character reference with ID {characterReferenceId} added successfully.", characterReference.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new character reference: {errorMessage}", ex.Message);
                throw;
            }
        }
    }
}