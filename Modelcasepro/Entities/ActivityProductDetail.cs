using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityProductDetail
{
    public int Id { get; set; }

    public string? Shift { get; set; }

    public DateTime? Date { get; set; }

    public string? SummaryOfWorks { get; set; }

    public int? ActivityId { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
