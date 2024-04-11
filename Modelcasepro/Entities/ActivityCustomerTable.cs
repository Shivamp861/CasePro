using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityCustomerTable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ContactNo { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? Postcode { get; set; }

    public string? Country { get; set; }

    public int? ActivityId { get; set; }

    public virtual ActivityTable? Activity { get; set; }
}
