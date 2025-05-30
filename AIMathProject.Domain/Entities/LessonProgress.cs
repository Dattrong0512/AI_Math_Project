﻿using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class LessonProgress
{
    public int LearningProgressId { get; set; }

    public int? LessonId { get; set; }

    public int? EnrollmentId { get; set; }

    public int? Process { get; set; }

    public string? Status { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual Lesson? Lesson { get; set; }
}
