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
                FirstName = s.FirstName,
                LastName = s.LastName,
                JobTitle = s.JobTitle,
                CandidateFirstName = s.CandidateFirstName,
                CandidateLastName = s.CandidateLastName
            }).ToList();

            return data;
        }

        public List<CharacterReferenceViewModel> RetrieveResponses()
        {
            var data = _repository.RetrieveAll().Select(s => new CharacterReferenceViewModel
            {
                Id = s.Id,
                CandidateFirstName = s.CandidateFirstName,
                CandidateLastName = s.CandidateLastName,
                Position = s.Position,
                RelationshipDuration = s.RelationshipDuration,
                Relationship = s.Relationship,
                CharacterEthics = s.CharacterEthics,
                Qualifications = s.Qualifications,
                FirstName = s.FirstName,
                LastName = s.LastName,
                JobTitle = s.JobTitle,
                ReasonToHire = s.ReasonToHire,
                CreatedTime = s.CreatedTime
            }).ToList();

            return data;
        }
    }
}
