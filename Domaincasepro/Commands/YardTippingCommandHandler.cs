using Domaincasepro.Repository;
using Microsoft.AspNetCore.Http;
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
    public class YardTippingCommandHandler
    {
        private readonly CaseproDbContext _context;

        public YardTippingCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public ActivityDetailResponseModel AddyardTippingdetails(YardTippingRequestModel yardTippingrequestModel, IActivitydetailsRepository activitydetailsrepo, IFormFile SiteImage)
        {
            try
            {
                ActivityDetail activityEntity = MapToEntitySite(yardTippingrequestModel);

                ActivityDetail addedActivity = activitydetailsrepo.AddOrUpdateActivitydetails(activityEntity);
                ActivityImage activityImage = MapToEntityImage(SiteImage, yardTippingrequestModel.Activityid);
                ActivityImage addactivityImage = activitydetailsrepo.AddOrUpdateActivityImage(activityImage);

                if (addedActivity != null)
                {
                    return siteInstallationResponseFactory.Create(true, "Yard Tipping successfully Added");
                }
                else
                {
                    // Something went wrong while adding the activity
                    return siteInstallationResponseFactory.Create(false, "Failed to Yard Tipping");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Yard Tipping: " + ex.Message);
            }
        }

        private ActivityDetail MapToEntitySite(YardTippingRequestModel requestModel)
        {
            return new ActivityDetail
            {
                Startandfinishtime= requestModel.Startandfinishtime,
                LiftingEquipmentUsed= requestModel.LiftingEquipmentUsed,
                ChainLiftingequipmenttobeused= requestModel.ChainLiftingequipmenttobeused,
                IncidentReporting=requestModel.IncidentReporting,
                AnyNearMissOccurrences= requestModel.AnyNearMissOccurrences,
                BarrierConditionChecks= requestModel.BarrierConditionChecks,
                AllRelevantActivityRams= requestModel.AllRelevantActivityRams,
                ActivityId = requestModel.Activityid

            };
        }
        private ActivityImage MapToEntityImage(IFormFile Image, int activityid)
        {
            var uploadPath = "wwwroot/Upload/"; // Relative path within the wwwroot folder
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var fullPath = Path.Combine(uploadPath, uniqueFileName);

            // Save the file to the server
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }

            // Construct the full URL of the uploaded image
            var baseUrl = "https://localhost:7007"; // Your application's base URL
            var fullUrl = baseUrl + "/" + fullPath.Replace("\\", "/").Replace("wwwroot/","");

            return new ActivityImage
            {
                ImageName = uniqueFileName,
                UploadedDate = DateTime.Now,
                UploadPath = fullUrl,
                ActivityId = activityid
            };
        }
    }
}
