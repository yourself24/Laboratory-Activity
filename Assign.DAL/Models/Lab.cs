using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Lab
{
    public int Lid { get; set; }

    public int LabNo { get; set; }

    public DateTime Date { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; } = new List<Assignment>();

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();
}
