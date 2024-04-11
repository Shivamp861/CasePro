using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityImage
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public string? ImageName { get; set; }

    public string? UploadPath { get; set; }

    public DateTime? UploadedDate { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
