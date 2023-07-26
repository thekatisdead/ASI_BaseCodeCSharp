using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ICharacterReferenceService
    {
        /// <summary>
        /// Retrieves all character references.
        /// </summary>
        /// <returns>A list of CharacterReferenceViewModel.</returns>
        List<CharacterReferenceViewModel> RetrieveAll();

        List<CharacterReferenceViewModel> RetrieveResponses();

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="characterReference">The character reference to add.</param>
        void AddCharacterReference(CharacterReferenceViewModel characterReference);
    }
}
