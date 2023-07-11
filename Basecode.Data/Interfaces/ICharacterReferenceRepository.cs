﻿using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface ICharacterReferenceRepository
    {
        IQueryable<CharacterReference> RetrieveAll();
        CharacterReference GetById(int id);
        void Add(CharacterReference characterReference);
    }
}
