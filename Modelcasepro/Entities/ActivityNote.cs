using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityNote
{
    public int Id { get; set; }

    public string? Notes { get; set; }

    public int? ActivityId { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
