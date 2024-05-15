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
    public class ActivityCommandHandler
    {
        private readonly IActivityRepository _repo;

        public ActivityCommandHandler(IActivityRepository repo)
        {
            _repo = repo;
        }

        public  ActivityResponseModel AddActivity(JobCard activityRequest)
        {
            try
            {
                ActivityTable activityEntity = MapToEntity(activityRequest);
                ActivityTable addedActivity = _repo.AddOrUpdateActivity(activityEntity);

                if (addedActivity != null)
                {
                    if (activityRequest.ActivityId==0)
                    {
                        return ActivityResponseFactory.Create(true, "Activity Added successfully", addedActivity.Id, addedActivity.ActivitType);
                    }
                    else
                    {
                        return ActivityResponseFactory.Create(true, "Activity Updated successfully", addedActivity.Id, addedActivity.ActivitType);
                    }
                    
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

		public ActivityResponseModel cloneActivity(JobCard activityRequest)
		{
			try
			{
               
				ActivityTable activityEntity = MapToEntity(activityRequest);
				(ActivityTable addedActivity,int flag) = _repo.CloneAddOrUpdateActivity(activityEntity);

				if (addedActivity != null)
				{
					if (flag == 1)
					{
						return ActivityResponseFactory.Create(true, "Activity Added successfully", addedActivity.Id, addedActivity.ActivitType);
					}
					else
					{
						return ActivityResponseFactory.Create(true, "Activity Updated successfully", addedActivity.Id, addedActivity.ActivitType);
					}

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

		public ActivityTable Updateactivitystatus(string status, int activityId)
        {
            var updatestatus = _repo.updateactivitystatus(status, activityId);
            return updatestatus;
        }

      

        private ActivityTable MapToEntity(JobCard requestModel)
        {
            return new ActivityTable
            {
                CustomerOrderNumber = requestModel.CustomerOrderNumber,
                SageOrderNumber = requestModel.SageOrderNumber,
                HcorderNumber = requestModel.HcorderNumber,
                DateRaised = requestModel.DateRaised.ToString(),
                RaisedBy = requestModel.RaisedBy,
                SiteAddress = requestModel.SiteAddress,
                OutorhoursEmrgContact = requestModel.OutofhoursEmrgContact,
                NearestAE = requestModel.NearestAE,
                ActivitType = requestModel.ActivityType,
                Id = requestModel.ActivityId,
                ActivityStatus = "Active",
                

            };
        }

        public ActivityResponseModel EditForDragDropCalander(int eventId, DateTime newDate)
        {
            try
            {
                bool resp = _repo.editForDragDropCalander(eventId, newDate);
                if (resp)
                {
                    return ActivityResponseFactory.Create(true, "Dropped successfully ", 0, null);
                }
                else
                {
                    return ActivityResponseFactory.Create(false, "Dropped Failed ", 0, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while delete Activity: " + ex.Message);
            }
        }
    }
}
