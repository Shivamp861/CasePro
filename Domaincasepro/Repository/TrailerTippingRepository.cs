using Microsoft.EntityFrameworkCore.Metadata;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class TrailerTippingRepository : ITrailerTippingRepository
    {
        private CaseproDbContext _context;
        public TrailerTippingRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public ActivityTrailerTable AddTrailerTipping(ActivityTrailerTable TrailerInfo)
        {
            try
            {
                _context.ActivityTrailerTables.Add(TrailerInfo);
                _context.SaveChanges();

                return TrailerInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ActivityTrailerTable> GetAllList()
        {
            // Retrieve all activities from the database
            return _context.ActivityTrailerTables.Where(x => x.IsOutBound == null).ToList();
        }
        public ActivityTrailerTable getActivityTrailerId(int id)
        {
            var latestEntity = _context.ActivityTrailerTables.OrderByDescending(x => x.Id == id).FirstOrDefault();
            return latestEntity;

        }
    }
}
