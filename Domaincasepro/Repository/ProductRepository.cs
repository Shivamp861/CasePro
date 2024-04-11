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
    }
}
