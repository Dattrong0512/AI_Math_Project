using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ExerciseDetailResult
{
    public int ExerciseDetailResultId { get; set; }

    public int? ExerciseDetailId { get; set; }

    public int? ExerciseResultId { get; set; }

    public bool? IsCorrect { get; set; }


    public string? QuestionType { get; set; }

    public virtual ExerciseDetail? ExerciseDetail { get; set; }

    public virtual ExerciseResult? ExerciseResult { get; set; }

    public virtual ICollection<UserChoiceAnswer>? UserChoiceAnswers { get; set; } = new List<UserChoiceAnswer>();

    public virtual ICollection<UserFillAnswer> UserFillAnswers { get; set; } = new List<UserFillAnswer>();

    public virtual ICollection<UserMatchingAnswer> UserMatchingAnswers { get; set; } = new List<UserMatchingAnswer>();
}
