using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class MatchingAnswer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string AnswerContent1 { get; set; } = null!;

    public string AnswerContent2 { get; set; } = null!;

    public virtual Question? Question { get; set; }
}
