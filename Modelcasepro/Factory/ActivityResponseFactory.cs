using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.Factory
{
    public class ActivityResponseFactory
    {
        public static ActivityResponseModel Create(bool success, string message,int activityid,string activityType)
        {
            return new ActivityResponseModel
            {
                IsSuccess = success,
                Message = message,
                ActivityId= activityid,
                ActivityType= activityType
            };
        }
	}
}
