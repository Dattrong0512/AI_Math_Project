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
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA41418431044B");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE874714344D");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17D256FA01");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__56B3DD81");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE63A55CF9B");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___5A846E65");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__33724318427ACDF6");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Question).WithMany(p => p.ChoiceAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Choice_An__quest__308E3499");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E79576879DDD8ADF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments).HasConstraintName("FK__Comment__lesson___52E34C9D");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comment__parent___50FB042B");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comment__user_id__51EF2864");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A496088F7");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___1D7B6025");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Error_Re__779B7C58527857B2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ErrorType).HasDefaultValue("unknown");
            entity.Property(e => e.Resolved).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___65F62111");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418E81E643CB");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__3DE82FB7");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C340CA472");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___exerc__40C49C62");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__41B8C09B");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0ACFCE7044");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__4959E263");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__4865BE2A");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__337243186DB80905");

            entity.HasOne(d => d.Question).WithMany(p => p.FillAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fill_Answ__quest__373B3228");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BEA51F6A43");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___251C81ED");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D33CCAD93");

            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__4D2A7347");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__4C364F0E");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__337243188A27F004");

            entity.HasOne(d => d.Question).WithMany(p => p.MatchingAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Matching___quest__345EC57D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F20B76D2B");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("unread");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___5F492382");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA419BBE77");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D627FA19C");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E5936CDA3E8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__19AACF41");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___18B6AB08");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___17C286CF");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549D922A830");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__2CBDA3B5");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02AE1B1AE5");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__27F8EE98");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D23258B16336D4");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__3B0BC30C");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__test___3A179ED3");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDA34B9737A");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__4589517F");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___44952D46");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B1AB990928");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D595F02E62C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__11158940");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__10216507");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___0F2D40CE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FE066656B");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
