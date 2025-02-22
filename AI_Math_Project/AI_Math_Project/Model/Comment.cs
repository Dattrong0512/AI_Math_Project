using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Comment")]
public partial class Comment
{
    [Key]
    [Column("comment_id")]
    public int CommentId { get; set; }

    [Column("parent_comment_id")]
    public int? ParentCommentId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("lesson_id")]
    public int? LessonId { get; set; }

    [Column("comment_content")]
    [StringLength(300)]
    public string? CommentContent { get; set; }

    [Column("status")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("ParentComment")]
    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    [ForeignKey("LessonId")]
    [InverseProperty("Comments")]
    public virtual Lesson? Lesson { get; set; }

    [ForeignKey("ParentCommentId")]
    [InverseProperty("InverseParentComment")]
    public virtual Comment? ParentComment { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User? User { get; set; }
}
