using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IInterviewerServices
    {
        public void Add(Interviewer interviewer);
        public List<InterviewerViewModel> GetAll();
        public void Update(Interviewer interviewer);
        public InterviewerViewModel GetById(int id);
    }
}
