using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class ActivitySummary
    {
        public string? ActivityType { get; set; }
        public List<Activitydetails> Activity { get; set; }
        public List<Trailerdetails> Trailer { get; set; }
        public List<Siteinstallation> Siteinstallations { get; set; }
        public List<ResourseData> ResourseDatas { get; set; }

    }
    public class Activitydetails
    {
        public DateTime? ActivityDate { get; set; }
        public string? Dayornight { get; set; }
        public string? customer { get; set; }
        public string? Type { get; set; }
        public string? SiteAddress { get; set; }
        public string? SummaryOfWorks { get; set; }
        //public string BarrierType { get; set; }
        //public string? BarrierQuntity { get; set; }
        //public string? MeetingSite { get; set; }


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
    public class Siteinstallation
    {
        public string? LabourSupplier { get; set; }

        public string? SupplierContact { get; set; }

        public string? NoOfPersoneSupplied { get; set; }

        public string? BarrierType { get; set; }

        public string? BarrierQty { get; set; }

        public string? BarrierStartAndFinishLocation { get; set; }
    }
    public class ResourseData
    {
        public string Shift { get; set; }
        public string Name { get; set; }

        public string ResourceType { get; set; }
        public string Comments { get; set; }

        public string DayNight { get; set; }
    }
}
