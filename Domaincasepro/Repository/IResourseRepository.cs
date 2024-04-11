using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface IResourseRepository
    {
        public bool Create(ActivityResourcesDetail resources);

        public ActivityTable getActivityId(ActivityTable activityid);
    }
}
