using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class UsersTable
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? ClientName { get; set; }

    public string? Password { get; set; }

    public bool? Isactive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastLogindate { get; set; }

    public virtual ICollection<ActivityTable> ActivityTables { get; } = new List<ActivityTable>();
}
