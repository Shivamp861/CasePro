using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface IActivitydetailsRepository
    {
        public ActivityDetail AddOrUpdateActivitydetails(ActivityDetail activitydetailsinfo);
        public ActivityImage AddOrUpdateActivityImage(ActivityImage activityimage);
    }
}
