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
    [StringLength(20)]
    [Unicode(false)]
    public string? QuestionType { get; set; }

    [Column("difficulty")]
    public short? Difficulty { get; set; }

    [Column("lesson_id")]
    public int? LessonId { get; set; }

    [Column("img_url")]
    [StringLength(300)]
    [Unicode(false)]
    public string? ImgUrl { get; set; }

    [Column("question_content")]
    [StringLength(255)]
    public string? QuestionContent { get; set; }

    [Column("pdf_solution")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PdfSolution { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<ChoiceAnswer> ChoiceAnswers { get; set; } = new List<ChoiceAnswer>();

    [InverseProperty("Question")]
    public virtual ICollection<ExerciseDetail> ExerciseDetails { get; set; } = new List<ExerciseDetail>();

    [InverseProperty("Question")]
    public virtual ICollection<FillAnswer> FillAnswers { get; set; } = new List<FillAnswer>();

    [ForeignKey("LessonId")]
    [InverseProperty("Questions")]
    public virtual Lesson? Lesson { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<MatchingAnswer> MatchingAnswers { get; set; } = new List<MatchingAnswer>();

    [InverseProperty("Question")]
    public virtual ICollection<TestDetail> TestDetails { get; set; } = new List<TestDetail>();
}
