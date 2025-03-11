using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Error_Report")]
public partial class ErrorReport
{
    [Key]
    [Column("error_id")]
    public int ErrorId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("error_message")]
    [StringLength(255)]
    public string? ErrorMessage { get; set; }

    [Column("error_type")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ErrorType { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("resolved")]
    public bool? Resolved { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ErrorReports")]
    public virtual User? User { get; set; }
}
