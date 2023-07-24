using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class CharacterReferenceRepository: BaseRepository, ICharacterReferenceRepository
    {
        public readonly BasecodeContext _context;

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
            return this.GetDbSet<CharacterReference>();
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="characterReference">The character reference to add.</param>
        public void Add(CharacterReference characterReference)
        {
            _context.CharacterReference.Add(characterReference);
            _context.SaveChanges();
        }
    }
}
