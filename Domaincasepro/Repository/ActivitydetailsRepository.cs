using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domaincasepro.Repository
{
    public class ActivitydetailsRepository : IActivitydetailsRepository
    {
        private CaseproDbContext _context;
        public ActivitydetailsRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public ActivityDetail AddOrUpdateActivitydetails(ActivityDetail data)
        {
            var existing = _context.ActivityDetails.Where(x => x.ActivityId == data.ActivityId).FirstOrDefault();

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(existing);
            }
            else
            {
                _context.ActivityDetails.Add(data);
            }

            _context.SaveChanges();

            return data;
        }

        public ActivityImage AddOrUpdateActivityImage(ActivityImage activityimage)
        {
            var existing = _context.ActivityImages.Where(x => x.Id == activityimage.Id && x.ActivityId == activityimage.ActivityId).FirstOrDefault();

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(activityimage);
            }
            else
            {
                _context.ActivityImages.Add(activityimage);
            }

            _context.SaveChanges();

            return activityimage;
        }

        public ActivityDetail GetActivityById(int activityid)
        {
            return _context.ActivityDetails.Where(x => x.ActivityId == activityid).FirstOrDefault();
        }

        public ActivityImage GetActivityImage(int activityid)
        {
            return _context.ActivityImages.Where(x => x.ActivityId == activityid).FirstOrDefault();
        }
    }
}
