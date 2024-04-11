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
    public class NotesQueryHandler
    {
        private readonly CaseproDbContext _context;
        public NotesQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public static ActivityTable GetId(ActivityRequestModel request, IActivityRepository activityrepository)
        {
            ActivityTable activityid = new ActivityTable
            {
                Id = request.ActivityId,
            };
            return activityrepository.getActivityId(activityid);
        }
    }
}
