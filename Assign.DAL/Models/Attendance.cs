using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Attendance
{
    public int AttId { get; set; }

    public int? LabId { get; set; }

    public int? StudId { get; set; }

    public bool? Present { get; set; }

    public virtual Lab? Lab { get; set; }

    public virtual Student? Stud { get; set; }
}
