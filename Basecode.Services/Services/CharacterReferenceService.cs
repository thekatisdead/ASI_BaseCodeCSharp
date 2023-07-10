using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class CharacterReferenceService: ICharacterReferenceService
    {
        private readonly ICharacterReferenceRepository _repository;

        public CharacterReferenceService(ICharacterReferenceRepository repository)
        {
            _repository = repository;
        }
    }
}
