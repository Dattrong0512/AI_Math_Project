using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class MatchingAnswer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string CorrectAnswer { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public virtual Question? Question { get; set; }
}
