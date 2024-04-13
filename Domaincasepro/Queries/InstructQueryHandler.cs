using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domaincasepro.Queries
{
    public class InstructQueryHandler
    {
        private readonly IInstructOperationRepository _repo;
        private readonly IActivityRepository _Activityrepo;

        public InstructQueryHandler(IInstructOperationRepository repo, IActivityRepository activityrepo)
        {
            _repo = repo;
            _Activityrepo = activityrepo;
        }
        public InstructOperation GetInstructDetails(int activityId)
        {
            try
            {
                var activity = ExecuteInstructQueryById(activityId);
                var activitydetails = ExecuteActivityQueryById(activityId);
                List<InstructorName> InstructName = GetInstructerName();

                return new InstructOperation()
                {
                    ActivityId = activitydetails.Id,
                    SelectedActivity = activity.SelectedActivity,
                    Name = activity.Name,
                    Date = activity.Date,
                    Note = activity.Note,
                    InstructorNames = InstructName,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }


        }

        public List<InstructorName> GetInstructerName()
        {
            return _repo.GetInstrutNameList() ?? new List<InstructorName>();
        }

        public ActivityTable ExecuteActivityQueryById(int activityid)
        {
            return _Activityrepo.GetActivityById(activityid) ?? new ActivityTable();
        }
        public List<InstructorFormDetail> GetInstructOperationsDetails(int activityid)
        {
            var instructdata = _repo.GetInstructorOperationsDetails(activityid);
            return instructdata;
        }
        public InstructorFormDetail ExecuteInstructQueryById(int activityId)
        {
            return _repo.GetInstructById(activityId) ?? new InstructorFormDetail();
        }

        public List<InstructorFormDetail> getinstructoperationsdetails(int activityid, InstructQueryHandler instructhandler)
        {
            var instructdata = _repo.GetInstructorOperationsDetails(activityid);
            return instructdata;
        }

        public InstructorName GetMailByName(string iname)
        {
            return _repo.GetMailByName(iname);
        }
    }
}
