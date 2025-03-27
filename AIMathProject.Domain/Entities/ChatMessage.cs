using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public partial class ChatMessage
{
    public int MessageId { get; set; }

    public int? ChatId { get; set; }

    public string? MessageDirection { get; set; }

    public string? MessageContent { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual Chat? Chat { get; set; }
}
