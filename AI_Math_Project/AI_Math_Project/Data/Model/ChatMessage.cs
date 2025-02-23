using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data.Model;

[Table("Chat_Message")]
public partial class ChatMessage
{
    [Key]
    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("chat_id")]
    public int? ChatId { get; set; }

    [Column("message_direction")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MessageDirection { get; set; }

    [Column("message_content")]
    [StringLength(255)]
    public string? MessageContent { get; set; }

    [Column("sent_at", TypeName = "datetime")]
    public DateTime? SentAt { get; set; }

    [ForeignKey("ChatId")]
    [InverseProperty("ChatMessages")]
    public virtual Chat? Chat { get; set; }
}
