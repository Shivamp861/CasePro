using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class CustomerQueryHandler
    {
        private readonly CaseproDbContext _context;
        public CustomerQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
     

    }
}
