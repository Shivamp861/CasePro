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
        bool Create(ActivityResourcesDetail request);

        //public ActivityTable getActivityId(ActivityTable activityid);
        public List<ActivityResourcesDetail> getResourseById(int activityId);
        bool Update(ActivityResourcesDetail aRD, int rid);
    }
}
