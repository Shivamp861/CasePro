using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class LoginQueryHandler
    {
        private readonly CaseproDbContext _context;

        public LoginQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public static UsersTable ExecuteLoginQuery(LoginRequestModel request, ILoginRepository loginRepository)
        {
            UsersTable newuser = new UsersTable
            {
                ClientName = request.ClientName,
                Username = request.Username,
                Password = request.Password,
            };

            // Execute the authentication query
            return loginRepository.Authentication(newuser);
        }
    }
}
