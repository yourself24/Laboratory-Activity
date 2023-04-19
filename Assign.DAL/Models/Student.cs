using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Student
{
    public int Sid { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public int GroupNo { get; set; }

    public string Hobby { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();
}
