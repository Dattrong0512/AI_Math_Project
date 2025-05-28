using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AIMathProject.Domain.Entities;

public class User : IdentityUser<int>
{
    public bool? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Avatar { get; set; }

    public bool? Status { get; set; }
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiredAtUtc { get; set; }

    public virtual ICollection<Chat> ChatSupportAgents { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatUsers { get; set; } = new List<Chat>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<ErrorReport> ErrorReports { get; set; } = new List<ErrorReport>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    public static User Create(string username, string email, DateTime dob, string phonenumber)
    {
        return new User
        {
            UserName = username,
            Email = email,
            Dob = dob,
            PhoneNumber = phonenumber
        };
    }
    public override string ToString()
    {
        return $"User is: {Email} has Id: {Id} ";
    }
}

