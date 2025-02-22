using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Test_Result")]
public partial class TestResult
{
    [Key]
    [Column("test_result_id")]
    public int TestResultId { get; set; }

    [Column("test_id")]
    public int? TestId { get; set; }

    [Column("enrollment_id")]
    public int? EnrollmentId { get; set; }

    [Column("score", TypeName = "decimal(2, 2)")]
    public decimal? Score { get; set; }

    [Column("completion_time")]
    public short? CompletionTime { get; set; }

    [Column("done_at", TypeName = "datetime")]
    public DateTime? DoneAt { get; set; }

    [ForeignKey("EnrollmentId")]
    [InverseProperty("TestResults")]
    public virtual Enrollment? Enrollment { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("TestResults")]
    public virtual Test? Test { get; set; }
}
