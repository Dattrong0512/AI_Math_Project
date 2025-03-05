using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Test_Detail")]
public partial class TestDetail
{
    [Key]
    [Column("test_detail_id")]
    public int TestDetailId { get; set; }

    [Column("test_id")]
    public int? TestId { get; set; }

    [Column("question_id")]
    public int? QuestionId { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("TestDetails")]
    public virtual Question? Question { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("TestDetails")]
    public virtual Test? Test { get; set; }

    [InverseProperty("TestDetail")]
    public virtual ICollection<TestDetailResult> TestDetailResults { get; set; } = new List<TestDetailResult>();
}
