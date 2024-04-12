using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class InstructorResponseFactory
    {
        public static InstructorResponseModel Create(bool isSuccess, string errorMessage, int id)
        {
            return new InstructorResponseModel
            {
                ActivityId = id,
                IsSuccess = isSuccess,
                Message = errorMessage
            };
        }
    }
}
