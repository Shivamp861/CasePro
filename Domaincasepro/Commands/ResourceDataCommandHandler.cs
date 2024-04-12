using Azure.Core;
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
    public class ResourceDataCommandHandler
    {
        private readonly IResourseRepository _resourseRepo;

        public ResourceDataCommandHandler(IResourseRepository resourseRepo)
        {
            _resourseRepo = resourseRepo;
        }
        public ActivityResponseModel Create(ResourseDetails request)
        {
            ActivityResourcesDetail ARD = new ActivityResourcesDetail
            {
                Shift = request.Shift,
                Name = request.Name,
                ResourceType = request.ResourceType,
                Comments = request.Comments,
                DayNight = request.DayNight,
                ActivityId = request.ActivityId,
                Date = request.Date,
            };
            bool Resource = _resourseRepo.Create(ARD);
            if (Resource)
            {
                return ResourceResponseFactory.Create(true, "Data inserted successfully");
            }
            else
            {
                return ResourceResponseFactory.Create(false, "something went wrong,Please try again");
            }


        }

        public ActivityResponseModel update(int rid, ResourseDetails request)
        {
            ActivityResourcesDetail ARD = new ActivityResourcesDetail
            {
                Shift = request.Shift,
                Name = request.Name,
                ResourceType = request.ResourceType,
                Comments = request.Comments,
                DayNight = request.DayNight,
                ActivityId = request.ActivityId,
                Date = request.Date,
            };
            bool Resource = _resourseRepo.Update(ARD, rid);
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
