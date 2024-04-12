using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class LoginCommandHandler
    {
        private readonly ILoginRepository _loginRepo;

        public LoginCommandHandler(ILoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }
        public LoginResponseModel Execute(LoginRequestModel user, UsersTable res)
        {
            try
            {
                if (res != null && res.Password == user.Password)
                {
                    bool success = _loginRepo.UpdateLastLogin(res.Id);
                    if (success)
                    {
                        return LoginResponseFactory.Create(true, "Login successful");
                    }
                    else
                    {
                        return LoginResponseFactory.Create(false, "Something went wrong, Please try again!");
                    }
                }
                else
                {
                    // Authentication failed
                    return LoginResponseFactory.Create(false, "Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Execute Authorize User: " + ex.Message);
            }

        }


    }
}
