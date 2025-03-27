using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? ParentCommentId { get; set; }

    public int? UserId { get; set; }

    public int? LessonId { get; set; }

    public string? CommentContent { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Lesson? Lesson { get; set; }

    public virtual Comment? ParentComment { get; set; }

    public virtual User? User { get; set; }
}
