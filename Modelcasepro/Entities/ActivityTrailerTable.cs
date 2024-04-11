using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityTrailerTable
{
    public int Id { get; set; }

    public string? TrailerSupplier { get; set; }

    public string? TrailerNumber { get; set; }

    public string? Quantity { get; set; }

    public string? LoadDepot { get; set; }

    public string? DepartFrom { get; set; }

    public DateTime? Date { get; set; }

    public string? LoadedTippedBy { get; set; }

    public bool? IsOutBound { get; set; }

    public int? ActivityId { get; set; }

    public string? VehicleReg { get; set; }

    public string? Loadpositioned { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
