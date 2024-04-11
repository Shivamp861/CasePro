using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{

    

    public class ActivitySignOffQueryHandler
    {
        private readonly CaseproDbContext _context;

        public ActivitySignOffQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public static ActivityTable GetId(ActivityRequestModel request, IActivityRepository activityRepository)
        {
            ActivityTable activityid = new ActivityTable
            {
                Id = request.ActivityId,
            };
            return activityRepository.getActivityId(activityid);
        }


    }
}
