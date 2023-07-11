using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface ICharacterReferenceRepository
    {
        /// <summary>
        /// Retrieves all character references.
        /// </summary>
        /// <returns>An IQueryable of CharacterReference.</returns>
        IQueryable<CharacterReference> RetrieveAll();

        /// <summary>
        /// Retrieves a character reference by its ID.
        /// </summary>
        /// <param name="id">The ID of the character reference.</param>
        /// <returns>The CharacterReference with the specified ID.</returns>
        CharacterReference GetById(int id);

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="characterReference">The character reference to add.</param>
        void Add(CharacterReference characterReference);
    }
}
