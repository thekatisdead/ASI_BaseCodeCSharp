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

        /// <summary>
        /// Initializes a new instance of the CharacterReferenceService class.
        /// </summary>
        /// <param name="repository">The character reference repository.</param>
        /// <param name="mapper">The mapper used for object mapping.</param>
        public CharacterReferenceService(ICharacterReferenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new character reference.
        /// </summary>
        /// <param name="characterReference">The character reference to add.</param>
        public void AddCharacterReference(CharacterReferenceViewModel characterReference)
        {
            characterReference.CreatedTime = DateTime.Now;
            characterReference.CreatedBy = System.Environment.UserName;

            _repository.Add(_mapper.Map<CharacterReference>(characterReference));
        }

        /// <summary>
        /// Retrieves all character references.
        /// </summary>
        /// <returns>A list of CharacterReferenceViewModel.</returns>
        public List<CharacterReferenceViewModel> RetrieveAll()
        {
            var data = _repository.RetrieveAll().Select(s => new CharacterReferenceViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName ?? string.Empty,
                LastName = s.LastName ?? string.Empty,
                JobTitle = s.JobTitle ?? string.Empty,
                CandidateFirstName = s.CandidateFirstName ?? string.Empty,
                CandidateLastName = s.CandidateLastName ?? string.Empty
            }).ToList();

            return data;
        }
    }
}
