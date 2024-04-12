using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class ResourceQueryHandler
    {
        public readonly IResourseRepository _resourseRepo;

        public ResourceQueryHandler(IResourseRepository resourseRepo)
        {
            _resourseRepo = resourseRepo;
        }
        public List<ResourseDetails> GetResourseDetails(int activityId)
        {
            try
            {
                var activity = ExecuteResourseQueryById(activityId);
                List<ResourseDetails> resourseDetails = activity.Select(resourseDetails => new ResourseDetails
                {
                    Id = resourseDetails.Id,
                    Name = resourseDetails.Name,
                    ResourceType = resourseDetails.ResourceType,
                    Comments = resourseDetails.Comments,
                    DayNight = resourseDetails.DayNight,
                    Shift = resourseDetails.Shift,
                    ActivityId = (int)resourseDetails.ActivityId,
                }).ToList();
              
                return resourseDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        private List<ActivityResourcesDetail> ExecuteResourseQueryById(int activityId)
        {
            return _resourseRepo.getResourseById(activityId) ?? new List<ActivityResourcesDetail>();
        }

    }
}
