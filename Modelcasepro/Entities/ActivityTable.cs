using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class ActivityTable
{
    public int Id { get; set; }

    public string? CustomerOrderNumber { get; set; }

    public string? SageOrderNumber { get; set; }

    public string? HcorderNumber { get; set; }

    public string? DateRaised { get; set; }

    public string? RaisedBy { get; set; }

    public string? OutorhoursEmrgContact { get; set; }

    public string? NearestAE { get; set; }

    public string? ActivitType { get; set; }

    public int? CustomerId { get; set; }

    public int? SignOffId { get; set; }

    public bool? IsActive { get; set; }

    public int? UserId { get; set; }

    public string? SiteAddress { get; set; }

    public virtual ICollection<ActivityCustomerTable> ActivityCustomerTables { get; } = new List<ActivityCustomerTable>();

    public virtual ICollection<ActivityDetail> ActivityDetails { get; } = new List<ActivityDetail>();

    public virtual ICollection<ActivityImage> ActivityImages { get; } = new List<ActivityImage>();

    public virtual ICollection<ActivityNote> ActivityNotes { get; } = new List<ActivityNote>();

    public virtual ICollection<ActivityProductDetail> ActivityProductDetails { get; } = new List<ActivityProductDetail>();

    public virtual ICollection<ActivityResourcesDetail> ActivityResourcesDetails { get; } = new List<ActivityResourcesDetail>();

    public virtual ICollection<ActivitySignOffdetail> ActivitySignOffdetails { get; } = new List<ActivitySignOffdetail>();

    public virtual ICollection<ActivityStatusTable> ActivityStatusTables { get; } = new List<ActivityStatusTable>();

    public virtual ICollection<ActivityTrailerTable> ActivityTrailerTables { get; } = new List<ActivityTrailerTable>();

    public virtual UsersTable? User { get; set; }
}
