using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface IProductRepository
    {
        public bool Create(ActivityProductDetail product);
        public ActivityTable getActivityId(ActivityTable activityid);
    }
}
