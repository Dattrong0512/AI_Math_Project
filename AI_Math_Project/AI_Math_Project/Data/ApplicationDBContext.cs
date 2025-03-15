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

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

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
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD91A59DBF");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA4141D9391516");

            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Admin).WithOne(p => p.Administrator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrators_admin_id");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE873F8C096A");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17AC3E9DC2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SupportAgent).WithMany(p => p.Chats).HasConstraintName("FK__Chat__support_ag__18B6AB08");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__17C286CF");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE67CB7CDF2");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___1C873BEC");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__33724318A93E1591");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Question).WithMany(p => p.ChoiceAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Choice_An__quest__69FBBC1F");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E7957687157A041B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments).HasConstraintName("FK__Comment__lesson___13F1F5EB");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comment__parent___1209AD79");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comment__user_id__12FDD1B2");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A435E9AAB");

            entity.ToTable("Enrollment", tb => tb.HasTrigger("trg_create_lesson_progress"));

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___55F4C372");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__Error_Re__DA71E16C5FD964A9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ErrorType).HasDefaultValue("unknown");
            entity.Property(e => e.Resolved).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___27F8EE98");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418EB89E8F6A");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__7755B73D");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C8C933689");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails).HasConstraintName("FK__Exercise___exerc__7A3223E8");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__7B264821");
        });

        modelBuilder.Entity<ExerciseDetailResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailResultId).HasName("PK__Exercise__B2FE72298C6D7223");

            entity.HasOne(d => d.ExerciseDetail).WithMany(p => p.ExerciseDetailResults).HasConstraintName("FK__Exercise___exerc__09746778");

            entity.HasOne(d => d.ExerciseResult).WithMany(p => p.ExerciseDetailResults).HasConstraintName("FK__Exercise___exerc__0A688BB1");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0AA065DD5A");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__0697FACD");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__05A3D694");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__337243182AFA37F5");

            entity.HasOne(d => d.Question).WithMany(p => p.FillAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fill_Answ__quest__70A8B9AE");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BEF07E170C");

            entity.ToTable("Lesson", tb =>
                {
                    tb.HasTrigger("trg_after_delete_lesson");
                    tb.HasTrigger("trg_after_insert_lesson");
                });

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___5E8A0973");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D006C7677");

            entity.ToTable("Lesson_Progress", tb => tb.HasTrigger("trg_update_lesson_progress"));

            entity.Property(e => e.IsCompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__0E391C95");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__0D44F85C");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__337243189A1EB341");

            entity.HasOne(d => d.Question).WithMany(p => p.MatchingAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Matching___quest__6DCC4D03");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F90CAEB3E");

            entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("unread");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___214BF109");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA8872400E");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D11E68FF9");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E59C25288E9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__5224328E");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___51300E55");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___503BEA1C");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC2154908F9950B");

            entity.Property(e => e.ImgUrl).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__662B2B3B");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__RefreshT__46A222CDE7993923");

            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.Token).IsFixedLength();

            entity.HasOne(d => d.Account).WithOne(p => p.RefreshToken)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__accou__3BFFE745");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02BEE34519");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__6166761E");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D2325875257B53");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__74794A92");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails).HasConstraintName("FK__Test_Deta__test___73852659");
        });

        modelBuilder.Entity<TestDetailResult>(entity =>
        {
            entity.HasKey(e => e.TestDetailResultId).HasName("PK__Test_Det__37921712DF210BD7");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.TestDetailResults).HasConstraintName("FK__Test_Deta__test___01D345B0");

            entity.HasOne(d => d.TestResult).WithMany(p => p.TestDetailResults).HasConstraintName("FK__Test_Deta__test___02C769E9");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDAC446B3D9");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__7EF6D905");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___7E02B4CC");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B16AA5CEA4");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D59C9C0C6F4");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__498EEC8D");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__489AC854");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___47A6A41B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F46C46D82");

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
