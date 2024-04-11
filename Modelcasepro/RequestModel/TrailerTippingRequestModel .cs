using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class TrailerTippingRequestModel
    {
        public DateTime Date { get; set; }
        public string TrailerSupplier { get; set; }
        public string TrailerNumber { get; set; }
        public string Quantity { get; set; }
        public string VehicleReg { get; set; }
        public string LoadDepot { get; set; }
        public string LoadedTippedBy { get; set; }
        public int Activityid { get; set; } 

    }
}
