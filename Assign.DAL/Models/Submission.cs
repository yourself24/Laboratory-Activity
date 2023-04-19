using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Submission
{
    public int SubId { get; set; }

    public int? AsignId { get; set; }

    public int? StudId { get; set; }

    public int Grade { get; set; }

    public string? Link { get; set; }

    public virtual Assignment? Asign { get; set; }

    public virtual Student? Stud { get; set; }
}
