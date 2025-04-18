using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Lesson
{
    public int LessonId { get; set; }

    public short? LessonOrder { get; set; }

    public string LessonName { get; set; } = null!;

    public int? ChapterId { get; set; }

    public string? LessonVideoUrl { get; set; }
    public string? LessonPdfUrl {get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
