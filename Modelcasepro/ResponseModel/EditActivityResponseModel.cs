using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ResponseModel
{
    public class EditActivityResponseModel
    {
        public int ActivityId { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string SageOrderNumber { get; set; }
        public string HcorderNumber { get; set; }
        public string ActivityType { get; set; }
        public DateTime DateRaised { get; set; }
        public string RaisedBy { get; set; }
        public string OutofhoursEmrgContact { get; set; }
        public string NearestAE { get; set; }
        public string SiteAddress { get; set; }
        public string Notes { get; set; }
        public string MeetingSite { get; set; }
        public string LabourSupplier { get; set; }
        public string SupplierContact { get; set; }
        public string NoOfPersoneSupplied { get; set; }
        public string BarrierType { get; set; }
        public string BarrierQty { get; set; }
        public string BarrierStartAndFinishLocation { get; set; }
        public string BarrierPerformance { get; set; }
        public string LengthOfRuns { get; set; }
        public string AnchoringDetails { get; set; }
        public string Isapermittobreakgroundrequired { get; set; }
        public string ChainLiftingequipmenttobeused { get; set; }
        public string LiftingEquipmentUsed { get; set; }
        public string IncidentReporting { get; set; }
        public string OtherResourcesEquipmentUsed { get; set; }
        public string AnySpecialInstructions { get; set; }
        public string AllRelevantActivityRams { get; set; }
        public string AnyNearMissOccurrences { get; set; }
        public string BarrierConditionChecks { get; set; }
        public string Startandfinishtime { get; set; }
        public string ImageName { get; set; }
        public string UploadPath { get; set; }
        public List<CustomerviewModel> customerModel { get; set; }
        public List<Productviewmodel> ProductModel { get; set; }
        public List<ResourceviewModel> ResouorceModel { get; set; }
        public List<Signoffviewmodel> SignoffModel { get; set; }
        public List<Trailerviewmodel> TrailerModel { get; set; }
    }
    public class CustomerviewModel
    {
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
    }

    public class Productviewmodel
    {
        public string Shift { get; set; }
        public DateTime Date { get; set; }
        public string SummaryOfWorks { get; set; }
    }

    public class ResourceviewModel
    {
        public string ResourceShift { get; set; }
        public DateTime ResourceDate { get; set; }
        public string Name { get; set; }
        public string ResourceType { get; set; }
        public string Comments { get; set; }
        public string DayNight { get; set; }
    }

    public class Signoffviewmodel
    {
        public DateTime CompetionDate { get; set; }
        public string PrintName { get; set; }
        public DateTime SignOffDate { get; set; }
        public string Signature { get; set; }
        
    }

    public class Trailerviewmodel
    {
        public string TrailerSupplier { get; set; }
        public string TrailerNumber { get; set; }
        public string Quantity { get; set; }
        public string VehicleReg { get; set; }
        public string LoadDepot { get; set; }
        public string LoadedTippedBy { get; set; }
    }
}
