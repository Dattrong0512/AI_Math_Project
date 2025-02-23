using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Token_Package")]
public partial class TokenPackage
{
    [Key]
    [Column("token_package_id")]
    public int TokenPackageId { get; set; }

    [Column("package_name")]
    [StringLength(100)]
    public string PackageName { get; set; } = null!;

    [Column("tokens")]
    public int? Tokens { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [InverseProperty("TokenPackage")]
    public virtual ICollection<TokenTransaction> TokenTransactions { get; set; } = new List<TokenTransaction>();
}
