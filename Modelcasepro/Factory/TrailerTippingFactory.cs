using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class TrailerTippingFactory
    {
        public static TrailerTippingResponseModel Create(bool isSuccess, string errorMessage, int trid)
        {
            return new TrailerTippingResponseModel
            {
                trid=trid,
                IsSuccess = isSuccess,
                Message = errorMessage
            };

        }
    }
}
