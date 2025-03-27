using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ErrorReport
{
    public int ErrorId { get; set; }

    public int? UserId { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ErrorType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Resolved { get; set; }

    public virtual User? User { get; set; }
}
