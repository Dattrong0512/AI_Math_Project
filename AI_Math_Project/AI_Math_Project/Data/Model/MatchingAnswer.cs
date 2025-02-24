using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Matching_Answer")]
public partial class MatchingAnswer
{
    [Key]
    [Column("answer_id")]
    public int AnswerId { get; set; }

    [Column("question_id")]
    public int? QuestionId { get; set; }

    [Column("correct_answer")]
    [StringLength(100)]
    public string CorrectAnswer { get; set; } = null!;

    [Column("img_url")]
    [StringLength(100)]
    [Unicode(false)]
    public string ImgUrl { get; set; } = null!;

    [ForeignKey("QuestionId")]
    [InverseProperty("MatchingAnswers")]
    public virtual Question? Question { get; set; }
}
