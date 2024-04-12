using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface IInstructOperationRepository
    {
        public InstructorFormDetail GetInstructById(int activityid);
        public List<InstructorFormDetail> GetInstructorOperationsDetails(int id);
        public bool SaveMailData(InstructorFormDetail formDetail);
        public bool SendMailData(InstructorFormDetail formDetail, string email, int activityid, int InstructorId);
        public bool Update(bool? hasSentval);
        public bool Updatemaildata(bool? hasSubmittedval, int id);
    }
}
