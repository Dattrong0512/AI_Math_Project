using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Chapter")]
public partial class Chapter
{
    [Key]
    [Column("chapter_id")]
    public int ChapterId { get; set; }

    [Column("chapter_order")]
    public short? ChapterOrder { get; set; }

    [Column("chapter_name")]
    [StringLength(100)]
    public string ChapterName { get; set; } = null!;

    [Column("grade")]
    public short? Grade { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [InverseProperty("Chapter")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    [InverseProperty("Chapter")]
    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
