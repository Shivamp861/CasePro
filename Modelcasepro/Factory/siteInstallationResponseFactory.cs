using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class siteInstallationResponseFactory
    {
        public static ActivityDetailResponseModel Create(bool isSuccess, string errorMessage)
        {
            return new ActivityDetailResponseModel
            {
                IsSuccess = isSuccess,
                Message = errorMessage
            };

        }
    }
}
