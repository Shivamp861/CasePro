using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityStatusTable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ActivityId { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
