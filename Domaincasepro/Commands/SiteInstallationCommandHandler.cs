using Domaincasepro.Repository;
using Microsoft.AspNetCore.Http;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
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
        private readonly CaseproDbContext _context;

        public SiteInstallationCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public ActivityDetailResponseModel AddSitedetails(SiteInstallationRequestModel siterequestModel, IActivitydetailsRepository activitydetailsrepo, IFormFile SiteImage)
        {
            try
            {
                ActivityDetail activityEntity = MapToEntitySite(siterequestModel);

                ActivityDetail addedActivity = activitydetailsrepo.AddOrUpdateActivitydetails(activityEntity);
                ActivityImage activityImage = MapToEntityImage(SiteImage, siterequestModel.ActivityId);
                ActivityImage addactivityImage = activitydetailsrepo.AddOrUpdateActivityImage(activityImage);

                if (addedActivity != null)
                {
                    return siteInstallationResponseFactory.Create(true, "Site Installation successfully Added");
                }
                else
                {
                    // Something went wrong while adding the activity
                    return siteInstallationResponseFactory.Create(false, "Failed to Site Installation");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding activity: " + ex.Message);
            }
        }

        private ActivityDetail MapToEntitySite(SiteInstallationRequestModel requestModel)
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
                ActivityId = requestModel.ActivityId

            };
        }
        private ActivityImage MapToEntityImage(IFormFile Image, int activityid)
        {
            var uploadPath = "Upload/"; // Relative path within the wwwroot folder
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
