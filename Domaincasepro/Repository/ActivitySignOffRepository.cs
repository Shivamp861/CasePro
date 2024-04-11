using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ActivitySignOffRepository : IActivitySignOffRepository
    {
        private CaseproDbContext _context;
        public ActivitySignOffRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public  ActivitySignOffdetail AddOrUpdateSignOffdetails(ActivitySignOffdetail signoff)
        {
            var existingcustomer = _context.ActivitySignOffdetails.Where(x => x.Id == signoff.Id).FirstOrDefault();

            if (existingcustomer != null)
            {
                _context.Entry(existingcustomer).CurrentValues.SetValues(signoff);
            }
            else
            {
                _context.ActivitySignOffdetails.Add(signoff);
            }

             _context.SaveChangesAsync();

            return signoff;
        }
    }
}
