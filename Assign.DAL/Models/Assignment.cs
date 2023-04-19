using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Assignment
{
    public int Asid { get; set; }

    public int? Lid { get; set; }

    public string AsName { get; set; } = null!;

    public DateOnly Deadline { get; set; }

    public string AsDesc { get; set; } = null!;

    public virtual Lab? LidNavigation { get; set; }

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();
}
