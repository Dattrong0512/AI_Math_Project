using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Fill_Answer")]
public partial class FillAnswer
{
    [Key]
    [Column("answer_id")]
    public int AnswerId { get; set; }

    [Column("question_id")]
    public int? QuestionId { get; set; }

    [Column("correct_answer")]
    [StringLength(100)]
    public string CorrectAnswer { get; set; } = null!;

    [Column("position")]
    public short Position { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("FillAnswers")]
    public virtual Question? Question { get; set; }
}
