using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ResourseRepository : IResourseRepository
    {
        public readonly CaseproDbContext _context;
        public ResourseRepository (CaseproDbContext context)
        {
            _context = context;
        }
        public bool Create(ActivityResourcesDetail resources)
        {
            bool success = false;
            try
            {
                _context.ActivityResourcesDetails.Add(resources);
                _context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;
        }
        public ActivityTable getActivityId(ActivityTable activityid)
        {
            var latestEntity = _context.ActivityTables.OrderByDescending(x => x.Id).FirstOrDefault();
            return latestEntity;

        }
    }
}
