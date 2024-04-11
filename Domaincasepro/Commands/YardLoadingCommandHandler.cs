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
    public class YardLoadingCommandHandler
    {
        private readonly CaseproDbContext _context;
        public YardLoadingCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public ActivityResponseModel Addyarddetails(YardLoadingRequestModel sitedata, IActivitydetailsRepository activityDetailRepository, IFormFile Imagesite)
        {
            try
            {
                ActivityDetail activityEntity = MapToEntitySite(sitedata);

                ActivityDetail addedActivity = activityDetailRepository.AddOrUpdateActivitydetails(activityEntity);
                ActivityImage activityImage = MapToEntityImage(Imagesite, sitedata.ActivityId);
                ActivityImage addactivityImage = activityDetailRepository.AddOrUpdateActivityImage(activityImage);

                if (addedActivity != null)
                {
                    return ActivityResponseFactory.Create(true, "Yard Loading Saved Successfully", addedActivity.Id, "");
                }
                else
                {
                    // Something went wrong while adding the activity
                    return ActivityResponseFactory.Create(false, "Failed to add activity", 0, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding activity: " + ex.Message);
            }
        }


        private ActivityDetail MapToEntitySite(YardLoadingRequestModel requestModel)
        {
            return new ActivityDetail
            {
                LiftingEquipmentUsed = requestModel.LiftingEquipmentUsed,
                ChainLiftingequipmenttobeused = requestModel.ChainLiftingequipmenttobeused,
                IncidentReporting = requestModel.IncidentReporting,
                AnyNearMissOccurrences = requestModel.AnyNearMissOccurrences,
                BarrierConditionChecks = requestModel.BarrierConditionChecks,
                AllRelevantActivityRams = requestModel.AllRelevantActivityRams,
                Startandfinishtime = requestModel.Startandfinishtime,
                ActivityId = requestModel.ActivityId

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
            var fullUrl = baseUrl + "/" + fullPath.Replace("\\", "/");

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
