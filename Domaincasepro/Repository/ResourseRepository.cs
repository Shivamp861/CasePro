using Modelcasepro.Entities;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ResourseRepository : IResourseRepository
    {
        private CaseproDbContext _context;
        public ResourseRepository(CaseproDbContext context)
        {
            _context = context;
        }

        public bool Create(ActivityResourcesDetail request)
        {
            bool success = false;
            try
            {
                _context.ActivityResourcesDetails.Add(request);
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

        public List<ActivityResourcesDetail> getResourseById(int activityId)
        {
            return _context.ActivityResourcesDetails.Where(x => x.ActivityId == activityId).ToList();
        }

        public bool Update(ActivityResourcesDetail resourcesDetail, int rid)
        {
            bool success = false;
            try
            {
                var existingrow = _context.ActivityResourcesDetails.FirstOrDefault(p => p.Id == rid);
                if (existingrow != null)
                {

                    existingrow.Shift = resourcesDetail.Shift;
                    existingrow.Name = resourcesDetail.Name;
                    existingrow.ResourceType = resourcesDetail.ResourceType;
                    existingrow.Comments = resourcesDetail.Comments;
                    existingrow.DayNight = resourcesDetail.DayNight;
                    _context.SaveChanges();
                    success = true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
            return success;
        }
    }
}
