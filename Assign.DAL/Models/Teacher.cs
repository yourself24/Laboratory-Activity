using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Teacher
{
    public int Tid { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;
}
