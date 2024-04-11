using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityResourcesDetail
{
    public int Id { get; set; }

    public string? Shift { get; set; }

    public DateTime? Date { get; set; }

    public string? Name { get; set; }

    public string? ResourceType { get; set; }

    public int? ActivityId { get; set; }

    public string? Comments { get; set; }

    public string? DayNight { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
