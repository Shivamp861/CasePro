using Azure.Core;
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
    public class ActivityCommandHandler
    {
        private readonly CaseproDbContext _context;

        public ActivityCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public  async Task<ActivityResponseModel> AddActivityAsync(ActivityRequestModel activityRequest, IActivityRepository activityrepo)
        {
            try
            {
                ActivityTable activityEntity = MapToEntity(activityRequest);
                ActivityTable addedActivity = await activityrepo.AddOrUpdateActivity(activityEntity);

                if (addedActivity != null)
                {
                    return ActivityResponseFactory.Create(true, "Activity Added successfully", addedActivity.Id,addedActivity.ActivitType);
                }
                else
                {
                    // Something went wrong while adding the activity
                    return ActivityResponseFactory.Create(false, "Failed to add activity", 0,"");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding activity: " + ex.Message);
            }
        }

        private ActivityTable MapToEntity(ActivityRequestModel requestModel)
        {
            return new ActivityTable
            {
                CustomerOrderNumber = requestModel.CustomerOrderNumber,
                SageOrderNumber = requestModel.SageOrderNumber,
                HcorderNumber = requestModel.HcorderNumber,
                DateRaised = requestModel.DateRaised.ToString(),
                RaisedBy = requestModel.RaisedBy,
                SiteAddress=requestModel.SiteAddress,
                OutorhoursEmrgContact = requestModel.OutofhoursEmrgContact,
                NearestAE = requestModel.NearestAE,
                ActivitType = requestModel.ActivityType
                
            };
        }
    }
}
