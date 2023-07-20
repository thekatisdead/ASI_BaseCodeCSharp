﻿using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Interfaces
{
    public interface IInterviewerRepository
    {
        public void Add(Interviewer interviewer);
        public IQueryable<Interviewer> GetAll();
    }
}
