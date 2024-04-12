using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class ActivitySummary
    {
        public List<Activitydetails> Activity {  get; set; }
        public List<Trailerdetails> Trailer { get; set; }
    }
    public class Activitydetails
    {
        public DateTime ActivityDate { get; set; }
        public string Dayornight { get; set; }
        public string customer { get; set; }
        public string Type { get; set; }
        public string BarrierType { get; set; }
        public string? BarrierQuntity { get; set; }

    }
    public class Trailerdetails
    {
        public DateTime? Date { get; set; }
        public string? TrailerSupplier { get; set; }
        public string? TrailerNumber { get; set; }
        public string? Quantity { get; set; }
        public string? VehicleReg { get; set; }
        public string? LoadDepot { get; set; }
        public string? LoadedTippedBy { get; set; }
        public string? DepartFrom { get; set; }
        public bool? IsOutBound { get; set; }
        public string? LoadPositioned { get; set; }
    }
}
