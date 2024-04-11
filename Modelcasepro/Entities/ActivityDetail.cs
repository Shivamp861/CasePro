using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityDetail
{
    public int? ActivityId { get; set; }

    public int Id { get; set; }

    public DateTime? ActivityDate { get; set; }

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

    public virtual ActivityTable? Activity { get; set; }
}
