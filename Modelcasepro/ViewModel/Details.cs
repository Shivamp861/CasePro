using Microsoft.AspNetCore.Http;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class Details
    {
        public int? ActivityId { get; set; }

        public int Id { get; set; }

        public DateTime? ActivityDate { get; set; }

        public string? Type {  get; set; }

        public string? MeetingSite { get; set; }

        public string? LabourSupplier { get; set; }

        public string? SupplierContact { get; set; }

        public string? NoOfPersoneSupplied { get; set; }

        public string? BarrierType { get; set; }

        public string? BarrierQty { get; set; }

        public string? BarrierStartAndFinishLocation { get; set; }

        public string? BarrierPerformance { get; set; }

        public string? LengthOfRuns { get; set; }

        public string? AnchoringDetails { get; set; }

        public string? Isapermittobreakgroundrequired { get; set; }

        public string? ChainLiftingequipmenttobeused { get; set; }

        public string? LiftingEquipmentUsed { get; set; }

        public string? IncidentReporting { get; set; }

        public string? OtherResourcesEquipmentUsed { get; set; }

        public string? AnySpecialInstructions { get; set; }

        public string? AllRelevantActivityRams { get; set; }

        public string? AnyNearMissOccurrences { get; set; }

        public string? BarrierConditionChecks { get; set; }

        public string? Startandfinishtime { get; set; }

        public int Imgid { get; set; }
        public List<IFormFile> SiteImages { get; set; }

        public string UploadPath { get; set; }        

        public List<TrailerViewModel> Trailerdetails { get; set; }
        public List<ActivityImagesViewModel> activityImages  { get; set; }
    }
    public class TrailerViewModel
    {
        public int id { get; set; }
        public int? activityid { get; set; }
        public string? TrailerSupplier { get; set; }
        public string? TrailerNumber { get; set; }
        public string? Quantity { get; set; }
        public string? VehicleReg { get; set; }
        public DateTime? Date {  get; set; }
        public string? DepartFrom {  get; set; }
        public string? LoadDepot { get; set; }
        public string? LoadedTippedBy { get; set; }
        public string? LoadPositioned {  get; set; }
        public bool? IsOutBound { get; set;}
    }

    public class ActivityImagesViewModel
    {
        public int Imgid { get; set; }
        public string UploadPath { get; set; }
    }

}
