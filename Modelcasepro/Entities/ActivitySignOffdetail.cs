using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivitySignOffdetail
{
    public int Id { get; set; }

    public DateTime? CompetionDate { get; set; }

    public string? Signature { get; set; }

    public string? PrintName { get; set; }

    public int? ActivityId { get; set; }

    public DateTime? Date { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
