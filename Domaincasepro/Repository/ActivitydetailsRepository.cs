using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ActivitydetailsRepository : IActivitydetailsRepository
    {
        private CaseproDbContext _context;
        public ActivitydetailsRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public ActivityDetail AddOrUpdateActivitydetails(ActivityDetail activitydetailsinfo)
        {
            var existingActivity =  _context.ActivityDetails.Where(x => x.Id == activitydetailsinfo.Id).FirstOrDefault();

            if (existingActivity != null)
            {
                _context.Entry(existingActivity).CurrentValues.SetValues(activitydetailsinfo);
            }
            else
            {
                _context.ActivityDetails.Add(activitydetailsinfo);
            }

             _context.SaveChangesAsync();

            return activitydetailsinfo;
        }

        public ActivityImage AddOrUpdateActivityImage(ActivityImage activityimage)
        {
            _context.ActivityImages.Add(activityimage);
            _context.SaveChangesAsync();

            return activityimage;
        }


    }
}
