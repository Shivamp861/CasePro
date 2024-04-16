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
    public class SignoffCommadnHandler
    {
        private readonly IActivitySignOffRepository _signoffrepo;
        public SignoffCommadnHandler(IActivitySignOffRepository signoffrepo)
        {
            _signoffrepo = signoffrepo;
        }
        public ActivityResponseModel AddSignoffDetailsAsync(List<SignoffRequestModel> signOffData)
        {
            try
            {
                bool success = true;
                if (signOffData!=null)
                {
                    foreach (var item in signOffData)
                    {
                        if (item.CompetionDate != DateTime.MinValue && item.PrintName != null && item.Signature != null && item.SignOffDate != DateTime.MinValue)
                        {
                            ActivitySignOffdetail activityEntity = MapToEntity(item);
                            ActivitySignOffdetail addedActivity = _signoffrepo.AddOrUpdateSignOffdetails(activityEntity);

                            if (addedActivity == null)
                            {
                                // Something went wrong while adding the activity
                                success = false;
                                break; // Exit the loop early
                            }
                        }
                        
                    }
                }
               

                if (success)
                {
                    // All activities added successfully
                    return ActivityResponseFactory.Create(true, "Signoff details added successfully", 0, "");
                }
                else
                {
                    // Failed to add one or more activities
                    return ActivityResponseFactory.Create(false, "Failed to add one or more Signoff details", 0, "");
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
                ActivityId = requestModel.ActivityId
            };
        }
    }
}
