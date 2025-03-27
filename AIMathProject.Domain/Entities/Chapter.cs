using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public short? ChapterOrder { get; set; }

    public string ChapterName { get; set; } = null!;

    public short? Grade { get; set; }

    public short? Semester { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
