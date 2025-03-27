using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class FillAnswer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string CorrectAnswer { get; set; } = null!;

    public short Position { get; set; }

    public virtual Question? Question { get; set; }
}
