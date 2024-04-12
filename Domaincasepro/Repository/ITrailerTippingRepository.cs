using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface ITrailerTippingRepository
    {
        public ActivityTrailerTable AddTrailerTipping(ActivityTrailerTable TrailerInfo);

        List<ActivityTrailerTable> GetAllListById(int id);
        ActivityTrailerTable getActivityTrailerId(int id);

    }
}
