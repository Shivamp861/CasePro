using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private CaseproDbContext _context;
        public LoginRepository(CaseproDbContext context)
        {
            this._context = context;
        }
        public UsersTable Authentication(UsersTable user)
        {
            var res= _context.UsersTables.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password) && x.ClientName.Equals(user.ClientName)).FirstOrDefault();
           
            return res;
        }

        public bool UpdateLastLogin(int id)
        {
            bool success = false;
            var res = _context.UsersTables.Where(x => x.Id == id).FirstOrDefault();
            if (res != null)
            {
                res.LastLogindate = DateTime.Now;
                _context.SaveChanges();
                success = true;
            }
            return success;
        }
    }
}
