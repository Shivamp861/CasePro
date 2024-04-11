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
    public static class LoginCommandHandler 
    {
        public static LoginResponseModel Execute(LoginRequestModel request,UsersTable user, ILoginRepository loginRepository)
        {
            try
            {
                if (user != null && user.Password == request.Password)
                {
                    bool success = loginRepository.UpdateLastLogin(user.Id);
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
