using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class NotesCommandHandler
    {
        private readonly IActivityRepository _activityrepo;
        public NotesCommandHandler(IActivityRepository activityrepo)
        {
            _activityrepo = activityrepo;
        }
        public ActivityResponseModel Create(JobCard jobcard)
        {
            ActivityNote notedata = new ActivityNote
            {
                Notes = jobcard.Notes,
                ActivityId = jobcard.ActivityId,
            };
            bool Resource = _activityrepo.Create(notedata);
            if (Resource)
            {

                return ActivityResponseFactory.Create(true, "Data inserted successfully", jobcard.ActivityId,"");
            }
            else
            {
                return ActivityResponseFactory.Create(false, "something went wrong,Please try again", 0, "");
            }

        }
    }
}
