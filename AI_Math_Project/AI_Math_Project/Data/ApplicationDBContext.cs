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

    public virtual DbSet<Account> Accounts { get; set; }

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

    public virtual DbSet<ExerciseDetailResult> ExerciseDetailResults { get; set; }

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

    public virtual DbSet<TestDetailResult> TestDetailResults { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TokenPackage> TokenPackages { get; set; }

    public virtual DbSet<TokenTransaction> TokenTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD81A18FDD");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA414151F321EC");

            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Admin).WithOne(p => p.Administrator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrators_admin_id");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE87BE0CBD51");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B173DA1D58B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SupportAgent).WithMany(p => p.Chats).HasConstraintName("FK__Chat__support_ag__5812E165");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__571EBD2C");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE67859193A");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___5BE37249");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__337243185DA9A1FB");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Question).WithMany(p => p.ChoiceAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Choice_An__quest__2957F27C");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E79576879E38EA1A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments).HasConstraintName("FK__Comment__lesson___534E2C48");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comment__parent___5165E3D6");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comment__user_id__525A080F");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A2AE8675A");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___1550F9CF");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__Error_Re__DA71E16C66AB8ABC");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ErrorType).HasDefaultValue("unknown");
            entity.Property(e => e.Resolved).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___675524F5");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418EF372DCFF");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__36B1ED9A");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C2A7E69B5");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___exerc__398E5A45");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__3A827E7E");
        });

        modelBuilder.Entity<ExerciseDetailResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailResultId).HasName("PK__Exercise__B2FE7229C1F03BB6");

            entity.HasOne(d => d.ExerciseDetail).WithMany(p => p.ExerciseDetailResults).HasConstraintName("FK__Exercise___exerc__48D09DD5");

            entity.HasOne(d => d.ExerciseResult).WithMany(p => p.ExerciseDetailResults).HasConstraintName("FK__Exercise___exerc__49C4C20E");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0A0152F59F");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__45F4312A");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__45000CF1");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__337243187B612CB8");

            entity.HasOne(d => d.Question).WithMany(p => p.FillAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fill_Answ__quest__3004F00B");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BE432AA706");

            entity.ToTable("Lesson", tb => tb.HasTrigger("trg_after_insert_lesson"));

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___1DE63FD0");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D341ABB55");

            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__4D9552F2");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__4CA12EB9");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__3372431860F855BE");

            entity.HasOne(d => d.Question).WithMany(p => p.MatchingAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Matching___quest__2D288360");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FB347DB61");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("unread");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___60A82766");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA31F81F6C");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D4A524BA9");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E59D85E68AB");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__118068EB");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___108C44B2");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___0F982079");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549E197D9B6");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__25876198");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C0276EDC3A1");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__20C2AC7B");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D23258F69D5E39");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__33D580EF");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__test___32E15CB6");
        });

        modelBuilder.Entity<TestDetailResult>(entity =>
        {
            entity.HasKey(e => e.TestDetailResultId).HasName("PK__Test_Det__37921712138D3317");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.TestDetailResults).HasConstraintName("FK__Test_Deta__test___412F7C0D");

            entity.HasOne(d => d.TestResult).WithMany(p => p.TestDetailResults).HasConstraintName("FK__Test_Deta__test___4223A046");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDAF680CAF8");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__3E530F62");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___3D5EEB29");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B17FA8AF94");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D59D602714C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__08EB22EA");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__07F6FEB1");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___0702DA78");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F9421BEA3");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Balance).HasDefaultValue(0);
            entity.Property(e => e.IsPremium).HasDefaultValue(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
