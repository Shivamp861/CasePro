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
    public class ResourceQueryHandler
    {
        private readonly CaseproDbContext _context;
        public ResourceQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
        //public static UsersTable ExecuteLoginQuery(LoginRequestModel request, ILoginRepository loginRepository)

        public static ActivityTable GetId(ActivityRequestModel request, IResourseRepository resourseRepository)
        {
            ActivityTable activityid = new ActivityTable
            {
                Id = request.ActivityId,
            };
            return resourseRepository.getActivityId(activityid);
        }
    }
}
