using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Question")]
public partial class Question
{
    [Key]
    [Column("question_id")]
    public int QuestionId { get; set; }

    [Column("question_type")]
    [StringLength(100)]
    [Unicode(false)]
    public string QuestionType { get; set; } = null!;

    [Column("lesson_id")]
    public int? LessonId { get; set; }

    [Column("right_answer", TypeName = "text")]
    public string? RightAnswer { get; set; }

    [Column("question_content")]
    [StringLength(255)]
    public string? QuestionContent { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<ExerciseDetail> ExerciseDetails { get; set; } = new List<ExerciseDetail>();

    [ForeignKey("LessonId")]
    [InverseProperty("Questions")]
    public virtual Lesson? Lesson { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<TestDetail> TestDetails { get; set; } = new List<TestDetail>();
}
