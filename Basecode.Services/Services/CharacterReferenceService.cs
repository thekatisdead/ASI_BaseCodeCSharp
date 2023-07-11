using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
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
        private readonly IMapper _mapper;

        public CharacterReferenceService(ICharacterReferenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddCharacterReference(CharacterReferenceViewModel characterReference)
        {
            characterReference.CreatedTime = DateTime.Now;
            characterReference.CreatedBy = System.Environment.UserName;

            _repository.Add(_mapper.Map<CharacterReference>(characterReference));
        }
    }
}
