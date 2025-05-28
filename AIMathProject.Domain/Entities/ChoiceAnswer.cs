using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ChoiceAnswer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string? Content { get; set; }

    public bool? IsCorrect { get; set; }

    public string? ImgUrl { get; set; }

    public virtual ICollection<ExerciseDetailResult> ExerciseDetailResults { get; set; } = new List<ExerciseDetailResult>();

    public virtual Question? Question { get; set; }
}
