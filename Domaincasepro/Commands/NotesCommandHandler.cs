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
            (bool Resource,bool Hassave) = _activityrepo.Create(notedata);
            if (Resource)
            {
                if (Hassave)
                {
                    return ActivityResponseFactory.Create(true, "Notes inserted successfully", jobcard.ActivityId, "");
                }
                else
                {
                    return ActivityResponseFactory.Create(true, "Notes Updateded successfully", jobcard.ActivityId, "");
                }
                
            }
            else
            {
                return ActivityResponseFactory.Create(false, "something went wrong,Please try again", 0, "");
            }

        }
    }
}
