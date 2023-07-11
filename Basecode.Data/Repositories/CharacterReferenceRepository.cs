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

        public IQueryable<CharacterReference> RetrieveAll()
        {
            return this.GetDbSet<CharacterReference>();
        }

        public CharacterReference GetById(int id)
        {
            return _context.CharacterReference.Find(id);
        }

        public void Add(CharacterReference characterReference)
        {
            _context.CharacterReference.Add(characterReference);
            _context.SaveChanges();
        }
    }
}
