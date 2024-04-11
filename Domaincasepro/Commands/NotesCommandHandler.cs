using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class NotesCommandHandler
    {
        public static ActivityResponseModel Create(NoteRequestModel request, ActivityTable getresponse, IActivityRepository activityrepository)
        {
            ActivityNote notedata = new ActivityNote
            {
                Notes = request.Notes,
                ActivityId = getresponse.Id,
            };
            bool Resource = activityrepository.Create(notedata);
            if (Resource)
            {

                return ActivityResponseFactory.Create(true, "Data inserted successfully",0,"");
            }
            else
            {
                return ActivityResponseFactory.Create(false, "something went wrong,Please try again",0, "");
            }
           
        }
    }
}
