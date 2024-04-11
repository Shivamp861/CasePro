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
    public class ActivitySignoffCommadnHandler
    {
        private readonly CaseproDbContext _context;

        public ActivitySignoffCommadnHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public  ActivityResponseModel AddSignoffDetailsAsync(List<SignoffRequestModel> signoffInfo, IActivitySignOffRepository signoffrepo)
        {
            try
            {
                bool success = true;

                foreach (var item in signoffInfo)
                {

                    ActivitySignOffdetail activityEntity = MapToEntity(item);
                    ActivitySignOffdetail addedActivity =  signoffrepo.AddOrUpdateSignOffdetails(activityEntity);

                    if (addedActivity == null)
                    {
                        // Something went wrong while adding the activity
                        success = false;
                        break; // Exit the loop early
                    }
                }

                if (success)
                {
                    // All activities added successfully
                    return ActivityResponseFactory.Create(true, "Signoff details added successfully", 0,"");
                }
                else
                {
                    // Failed to add one or more activities
                    return ActivityResponseFactory.Create(false, "Failed to add one or more Signoff details", 0,"");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding details: " + ex.Message);
            }
        }

        private ActivitySignOffdetail MapToEntity(SignoffRequestModel requestModel)
        {
            return new ActivitySignOffdetail
            {
                Signature = requestModel.Signature,
                CompetionDate = requestModel.CompetionDate,
                Date = requestModel.SignOffDate,
                PrintName = requestModel.PrintName,
                ActivityId=requestModel.ActivityId
            };
        }
    }
}
