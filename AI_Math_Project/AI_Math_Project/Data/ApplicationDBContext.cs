using System;
using System.Collections.Generic;
using AI_Math_Project.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Data;

public partial class ApplicationDBContext : DbContext
{
    private readonly ILogger<ApplicationDBContext> logger;

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, ILogger<ApplicationDBContext> _logger)
        : base(options)
    {
        logger = _logger;
        logger.LogInformation("Tao thanh cong DB context");
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChoiceAnswer> ChoiceAnswers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<ErrorReport> ErrorReports { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseDetail> ExerciseDetails { get; set; }

    public virtual DbSet<ExerciseResult> ExerciseResults { get; set; }

    public virtual DbSet<FillAnswer> FillAnswers { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonProgress> LessonProgresses { get; set; }

    public virtual DbSet<MatchingAnswer> MatchingAnswers { get; set; }

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
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA41414DC76F9A");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE875300BA8F");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17AD931576");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__76B698BF");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE6CE6FF473");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___7A8729A3");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__33724318A73E23F5");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Question).WithMany(p => p.ChoiceAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Choice_An__quest__5090EFD7");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E7957687E87101C3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments).HasConstraintName("FK__Comment__lesson___72E607DB");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comment__parent___70FDBF69");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comment__user_id__71F1E3A2");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A6A72C244");

            entity.ToTable("Enrollment", tb => tb.HasTrigger("trg_create_lesson_progress"));

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___3C89F72A");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Error_Re__779B7C584FD7FA41");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ErrorType).HasDefaultValue("unknown");
            entity.Property(e => e.Resolved).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___05F8DC4F");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418EDBDA9EE8");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__5DEAEAF5");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69CDFA8774B");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___exerc__60C757A0");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__61BB7BD9");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0AEAA833EE");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__695C9DA1");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__68687968");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__33724318D0E56B13");

            entity.HasOne(d => d.Question).WithMany(p => p.FillAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fill_Answ__quest__573DED66");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BED8EF5663");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___451F3D2B");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D704527E1");

            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__6D2D2E85");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__6C390A4C");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__3372431823C02980");

            entity.HasOne(d => d.Question).WithMany(p => p.MatchingAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Matching___quest__546180BB");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F0C62EF8B");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("unread");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___7F4BDEC0");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA422C8880");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D22694F9B");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E59AA953456");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__38B96646");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___37C5420D");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___36D11DD4");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC2154919660484");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__4CC05EF3");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02511BBC70");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__47FBA9D6");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D232584B836D0A");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__5B0E7E4A");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__test___5A1A5A11");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDAB29DE9CA");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__658C0CBD");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___6497E884");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B19176696E");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D593DCAEA21");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__30242045");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__2F2FFC0C");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___2E3BD7D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF5192FE2");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
