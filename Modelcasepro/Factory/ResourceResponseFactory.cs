using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class ResourceResponseFactory
    {
        public static ActivityResponseModel Create(bool isSuccess, string errorMessage)
        {
            return new ActivityResponseModel
            {
                IsSuccess = isSuccess,
                Message = errorMessage
            };
        }
    }
}
