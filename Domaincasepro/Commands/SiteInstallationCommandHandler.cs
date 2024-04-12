using Domaincasepro.Repository;
using Microsoft.AspNetCore.Http;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Domaincasepro.Commands
{
    public class SiteInstallationCommandHandler
    {
        private readonly IActivitydetailsRepository _repo;

        public SiteInstallationCommandHandler(IActivitydetailsRepository repo)
        {
            _repo = repo;
        }
        public  ActivityDetailResponseModel AddSitedetails(Details siterequestModel, IFormFile SiteImage)
        {
            try
            {
                ActivityDetail activityEntity = MapToEntitySite(siterequestModel);
                
                ActivityDetail addedActivity = _repo.AddOrUpdateActivitydetails(activityEntity);
                ActivityImage activityImage = MapToEntityImage(SiteImage, siterequestModel.ActivityId ?? 0);
                ActivityImage addactivityImage = _repo.AddOrUpdateActivityImage(activityImage);

                if (addedActivity != null)
                {
                    if (siterequestModel.Id==0)
                    {
                        return siteInstallationResponseFactory.Create(true, "Activity details successfully Added", addedActivity.ActivityId ??0);
                    }
                    else
                    {
                        return siteInstallationResponseFactory.Create(true, "Activity details successfully Updated", addedActivity.ActivityId ?? 0);
                    }
                  
                }
                else
                {
                    // Something went wrong while adding the activity
                    return siteInstallationResponseFactory.Create(false, "Failed to Site Installation", 0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding activity: " + ex.Message);
            }
        }

        private  ActivityDetail MapToEntitySite(Details requestModel)
        {
            return new ActivityDetail
            {
                MeetingSite = requestModel.MeetingSite,
                LabourSupplier = requestModel.LabourSupplier,
                SupplierContact = requestModel.SupplierContact,
                NoOfPersoneSupplied = requestModel.NoOfPersoneSupplied, // Corrected property name
                BarrierType = requestModel.BarrierType,
                BarrierQty = requestModel.BarrierQty,
                BarrierStartAndFinishLocation = requestModel.BarrierStartAndFinishLocation,
                BarrierPerformance = requestModel.BarrierPerformance,
                LengthOfRuns = requestModel.LengthOfRuns,
                AnchoringDetails = requestModel.AnchoringDetails,
                Isapermittobreakgroundrequired = requestModel.Isapermittobreakgroundrequired, // Corrected property name
                ChainLiftingequipmenttobeused = requestModel.ChainLiftingequipmenttobeused, // Corrected property name
                IncidentReporting = requestModel.IncidentReporting,
                OtherResourcesEquipmentUsed = requestModel.OtherResourcesEquipmentUsed,
                AnySpecialInstructions = requestModel.AnySpecialInstructions,
                AllRelevantActivityRams = requestModel.AllRelevantActivityRams,
                ActivityId = requestModel.ActivityId,
                LiftingEquipmentUsed=requestModel.LiftingEquipmentUsed,
                AnyNearMissOccurrences=requestModel.AnyNearMissOccurrences,
                BarrierConditionChecks=requestModel.BarrierConditionChecks,
                Startandfinishtime = requestModel.Startandfinishtime
            };
        }
        private  ActivityImage MapToEntityImage(IFormFile Image, int activityid)
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
