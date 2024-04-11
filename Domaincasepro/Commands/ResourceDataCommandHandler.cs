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
    public class ResourceDataCommandHandler
    {
        public static ActivityResponseModel Create(ResourceRequestModel request, ActivityTable getresponse, IResourseRepository resourseRepository)
        {
            ActivityResourcesDetail resoursedata = new ActivityResourcesDetail
            {
                ResourceType = request.ResourceType,
                Name = request.Name,
                Shift = request.Shift,
                ActivityId = getresponse.Id,
                Comments = request.Comments,
                Date = request.Date,
                DayNight = request.DayNight,
            };
            bool Resource = resourseRepository.Create(resoursedata);
            if (Resource)
            {
                return ResourceResponseFactory.Create(true, "Data inserted successfully");
            }
            else
            {
                return ResourceResponseFactory.Create(false, "something went wrong,Please try again");
            }
        }
    }
}
