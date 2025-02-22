using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Model;

[Table("Chat")]
public partial class Chat
{
    [Key]
    [Column("chat_id")]
    public int ChatId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("support_agent_id")]
    public int? SupportAgentId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Chat")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    [ForeignKey("UserId")]
    [InverseProperty("Chats")]
    public virtual User? User { get; set; }
}
