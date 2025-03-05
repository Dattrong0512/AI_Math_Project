using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Test_Detail_Result")]
public partial class TestDetailResult
{
    [Key]
    [Column("test_detail_result_id")]
    public int TestDetailResultId { get; set; }

    [Column("test_detail_id")]
    public int? TestDetailId { get; set; }

    [Column("test_result_id")]
    public int? TestResultId { get; set; }

    [Column("is_correct")]
    public bool? IsCorrect { get; set; }

    [ForeignKey("TestDetailId")]
    [InverseProperty("TestDetailResults")]
    public virtual TestDetail? TestDetail { get; set; }

    [ForeignKey("TestResultId")]
    [InverseProperty("TestDetailResults")]
    public virtual TestResult? TestResult { get; set; }
}
