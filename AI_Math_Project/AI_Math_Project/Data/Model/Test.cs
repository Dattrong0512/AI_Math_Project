using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Test")]
public partial class Test
{
    [Key]
    [Column("test_id")]
    public int TestId { get; set; }

    [Column("chapter_id")]
    public int? ChapterId { get; set; }

    [Column("test_name")]
    [StringLength(100)]
    public string? TestName { get; set; }

    [ForeignKey("ChapterId")]
    [InverseProperty("Tests")]
    public virtual Chapter? Chapter { get; set; }

    [InverseProperty("Test")]
    public virtual ICollection<TestDetail> TestDetails { get; set; } = new List<TestDetail>();

    [InverseProperty("Test")]
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
