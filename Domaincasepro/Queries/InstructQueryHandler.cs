using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return new InstructOperation()
                {
                    ActivityId = activitydetails.Id,
                    SelectedActivity = activity.SelectedActivity,
                    Name = activity.Name,
                    Date = activity.Date,
                    Note = activity.Note,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }


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


    }
}
