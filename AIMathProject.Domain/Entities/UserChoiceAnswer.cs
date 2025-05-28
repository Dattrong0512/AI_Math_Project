using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class UserChoiceAnswer
{
    public int UserChoiceAnswerId { get; set; }

    public int? ExerciseDetailResultId { get; set; }

    public int? AnswerId { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual ExerciseDetailResult? ExerciseDetailResult { get; set; }
}