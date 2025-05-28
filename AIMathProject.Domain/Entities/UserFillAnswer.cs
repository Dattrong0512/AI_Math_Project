using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class UserFillAnswer
{
    public int UserFillAnswerId { get; set; }

    public int? ExerciseDetailResultId { get; set; }

    public string? WrongAnswer { get; set; }

    public int? Position { get; set; }

    public virtual ExerciseDetailResult? ExerciseDetailResult { get; set; }
}
