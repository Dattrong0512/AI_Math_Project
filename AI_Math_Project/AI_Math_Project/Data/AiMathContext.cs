using System;
using System.Collections.Generic;
using AI_Math_Project.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data;

public partial class AiMathContext : DbContext
{
    private readonly ILogger<AiMathContext> logger;

    //Receive a DbContextOptions and pass to parent class
    //DI injects 2 services into this class and the constructor
    //is only launched when there is a request to the service in HttpRequests or GetRequiredService(Take services 
    //has been register in DI
    public AiMathContext(DbContextOptions<AiMathContext> options,
        ILogger<AiMathContext> _logger) : base(options)
    {
        logger = _logger;
        logger.LogInformation("Database da duoc khoi tao");
        logger.LogInformation($"options: {options}");
    }
    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<ErrorReport> ErrorReports { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseDetail> ExerciseDetails { get; set; }

    public virtual DbSet<ExerciseResult> ExerciseResults { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonProgress> LessonProgresses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<PlanTransaction> PlanTransactions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestDetail> TestDetails { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TokenPackage> TokenPackages { get; set; }

    public virtual DbSet<TokenTransaction> TokenTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA4141239FF7C4");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE878BE7F29E");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17516F188E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__01142BA1");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE6E64A0DBD");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___04E4BC85");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E79576871B45C1AF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments).HasConstraintName("FK__Comment__lesson___7D439ABD");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comment__parent___7B5B524B");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comment__user_id__7C4F7684");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A9D26532A");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___5441852A");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Error_Re__779B7C58830E9D9E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ErrorType).HasDefaultValue("unknown");
            entity.Property(e => e.Resolved).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___10566F31");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418E036BA6F6");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__68487DD7");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C4AE73AA2");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___exerc__6B24EA82");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___quest__6C190EBB");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0AB3A29B44");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__73BA3083");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__72C60C4A");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BEB6F2B27C");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___5BE2A6F2");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D931A956C");

            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__778AC167");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__76969D2E");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FECCE3235");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("unread");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___09A971A2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA406F0D3E");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D93DD2FBD");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E5931726C0B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__5070F446");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___4E88ABD4");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549EB470766");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__619B8048");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02A497959E");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__5EBF139D");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D23258FFF2E1E6");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__quest__656C112C");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__test___6477ECF3");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDA61F2B047");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__6FE99F9F");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___6EF57B66");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B1A8EBA85E");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D59D6A2F707");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__47DBAE45");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F24F9886D");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
