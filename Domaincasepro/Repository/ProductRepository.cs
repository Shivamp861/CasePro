using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CaseproDbContext _context;
        public ProductRepository(CaseproDbContext context)
        {
            this._context = context;
        }
        public bool Create(ActivityProductDetail product)
        {
            bool success = false;
            try
            {
                _context.ActivityProductDetails.Add(product);
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

        public List<ActivityProductDetail> getProductById(int activityId)
        {
            return _context.ActivityProductDetails.Where(x => x.ActivityId == activityId).ToList();
        }

        public bool Update(ActivityProductDetail productdata, int pid)
        {
            bool success = false;
            try
            {
                var existingrow = _context.ActivityProductDetails.FirstOrDefault(p => p.Id == pid);
                if (existingrow != null)
                {
                    existingrow.Date = productdata.Date;
                    existingrow.Shift = productdata.Shift;
                    existingrow.SummaryOfWorks = productdata.SummaryOfWorks;
                    existingrow.ActivityId = productdata.ActivityId;
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
