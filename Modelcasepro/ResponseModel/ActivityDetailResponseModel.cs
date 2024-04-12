using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ResponseModel
{
    public class ActivityDetailResponseModel
    {
        public int? ActivityId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
