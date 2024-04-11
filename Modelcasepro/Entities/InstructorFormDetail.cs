using System;
using System.Collections.Generic;

namespace Modelcasepro.Entities;

public partial class InstructorFormDetail
{
    public int Id { get; set; }

    public string? SelectedActivity { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public string? Note { get; set; }

    public bool? HasSent { get; set; }

    public bool? HasSubmitted { get; set; }
}
