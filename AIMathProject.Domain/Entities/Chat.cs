using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class Chat
{
    public int ChatId { get; set; }

    public int? UserId { get; set; }

    public int? SupportAgentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual User? SupportAgent { get; set; }

    public virtual User? User { get; set; }
}
