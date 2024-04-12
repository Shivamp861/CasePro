using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class CustomerRequestModel
    {

        public int ActivityId { get; set; }
        public int custid { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
    }
}
