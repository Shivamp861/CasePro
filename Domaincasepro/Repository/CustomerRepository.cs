using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private CaseproDbContext _context;
        public CustomerRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public  ActivityCustomerTable AddOrUpdateCustomer(ActivityCustomerTable cusInfo)
        {
            try
            {
                var existingcustomer = _context.ActivityCustomerTables.Where(x => x.Id == cusInfo.Id && x.ActivityId==cusInfo.ActivityId).FirstOrDefault();

                if (existingcustomer != null)
                {
                    _context.Entry(existingcustomer).CurrentValues.SetValues(cusInfo);
                }
                else
                {
                    _context.ActivityCustomerTables.Add(cusInfo);
                }

                 _context.SaveChanges();

                return cusInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ActivityCustomerTable> GetActivityById(int activityid)
        {
            return _context.ActivityCustomerTables.Where(x => x.ActivityId == activityid).ToList();
        }
    }
}
