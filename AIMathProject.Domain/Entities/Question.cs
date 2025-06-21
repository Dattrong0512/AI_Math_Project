using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public string? QuestionType { get; set; }

    public short? Difficulty { get; set; }

    public int? LessonId { get; set; }

    public string? ImgUrl { get; set; }

    public string? QuestionContent { get; set; }

    public string? QuestionPrompt { get; set; }

    public string? PdfSolution { get; set; }

    public virtual ICollection<ChoiceAnswer> ChoiceAnswers { get; set; } = new List<ChoiceAnswer>();

    public virtual ICollection<ExerciseDetail> ExerciseDetails { get; set; } = new List<ExerciseDetail>();

    public virtual ICollection<FillAnswer> FillAnswers { get; set; } = new List<FillAnswer>();

    public virtual Lesson? Lesson { get; set; }

    public virtual ICollection<MatchingAnswer> MatchingAnswers { get; set; } = new List<MatchingAnswer>();

    public virtual ICollection<TestDetail> TestDetails { get; set; } = new List<TestDetail>();
}
