using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class LoginResponseFactory
    {
        public static LoginResponseModel Create(bool success, string message)
        {
            return new LoginResponseModel
            {
                Success = success,
                Message = message
            };
        }


    }
}
