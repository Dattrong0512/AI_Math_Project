using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Choice_Answer")]
public partial class ChoiceAnswer
{
    [Key]
    [Column("answer_id")]
    public int AnswerId { get; set; }

    [Column("question_id")]
    public int? QuestionId { get; set; }

    [Column("content")]
    [StringLength(100)]
    public string? Content { get; set; }

    [Column("is_correct")]
    public bool? IsCorrect { get; set; }

    [Column("img_url")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ImgUrl { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("ChoiceAnswers")]
    public virtual Question? Question { get; set; }
}
