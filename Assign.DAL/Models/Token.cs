using System;
using System.Collections.Generic;

namespace Assign.DAL.Models;

public partial class Token
{
    public int TokId { get; set; }

    public string Token1 { get; set; }
    
    public bool Used { get; set; }
}
