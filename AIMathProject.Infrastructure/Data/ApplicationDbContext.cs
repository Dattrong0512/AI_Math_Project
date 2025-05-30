using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AIMathProject.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<Domain.Entities.User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Domain.Entities.User> Users { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChoiceAnswer> ChoiceAnswers { get; set; }

    public virtual DbSet<CoinTransaction> CoinTransactions { get; set; }

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

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestDetail> TestDetails { get; set; }

    public virtual DbSet<TestDetailResult> TestDetailResults { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TokenPackage> TokenPackages { get; set; }

    public virtual DbSet<TokenTransaction> TokenTransactions { get; set; }

    public virtual DbSet<UserFillAnswer> UserFillAnswers { get; set; }
    public virtual DbSet<UserChoiceAnswer> UserChoiceAnswers { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<EnrollmentUnlockExercise> EnrollmentUnlockExercises { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");

     
      

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE87A842A01F");

            entity.ToTable("Chapter");

            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.ChapterName)
                .HasMaxLength(100)
                .HasColumnName("chapter_name");
            entity.Property(e => e.ChapterOrder).HasColumnName("chapter_order");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Semester).HasColumnName("semester");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17FD92E0DE");

            entity.ToTable("Chat");

            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SupportAgentId).HasColumnName("support_agent_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SupportAgent).WithMany(p => p.ChatSupportAgents)
                .HasForeignKey(d => d.SupportAgentId)
                .HasConstraintName("FK__Chat__support_ag__08B54D69");

            entity.HasOne(d => d.User).WithMany(p => p.ChatUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__09A971A2");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE6D7DE0D58");

            entity.ToTable("Chat_Message");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.MessageContent)
                .HasMaxLength(255)
                .HasColumnName("message_content");
            entity.Property(e => e.MessageDirection)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("message_direction");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat_Mess__chat___0A9D95DB");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__337243186EF893D4");

            entity.ToTable("Choice_Answer");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .HasColumnName("content");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("img_url");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.ChoiceAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Choice_An__quest__0B91BA14");
        });

        modelBuilder.Entity<CoinTransaction>(entity =>
        {
            entity.HasKey(e => e.CoinTransactionId).HasName("PK__Coin_Tra__6422D8A467FE1B99");

            entity.ToTable("Coin_Transaction");

            entity.Property(e => e.CoinTransactionId).HasColumnName("coin_transaction_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CoinRemains).HasColumnName("coin_remains");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IsTokenPackage).HasColumnName("is_token_package");
            entity.Property(e => e.TokenPackageId).HasColumnName("token_package_id");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.CoinTransactions)
                .HasForeignKey(d => d.TokenPackageId)
                .HasConstraintName("FK__Coin_Tran__token__3CF40B7E");

            entity.HasOne(d => d.Wallet).WithMany(p => p.CoinTransactions)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coin_Tran__walle__3EDC53F0");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E7957687FD12D4BC");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CommentContent)
                .HasMaxLength(300)
                .HasColumnName("comment_content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK__Comment__lesson___0C85DE4D");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comment__parent___0D7A0286");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__user_id__0E6E26BF");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A7906B3D7");

            entity.ToTable("Enrollment", tb =>
                {
                    tb.HasTrigger("trg_create_lesson_progress");
                    tb.HasTrigger("trg_update_grade_lesson_progress");
                });

            entity.HasIndex(e => new { e.UserId, e.Grade, e.Semester }, "UK_Enrollment_UserId_Grade_Semester").IsUnique();

            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.AvgScore)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("avg_score");
            entity.Property(e => e.EndYear).HasColumnName("end_year");
            entity.Property(e => e.EnrollmentDate).HasColumnName("enrollment_date");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.StartYear).HasColumnName("start_year");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Enrollmen__user___0F624AF8");
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__Error_Re__DA71E16CE3A540E3");

            entity.ToTable("Error_Report");

            entity.Property(e => e.ErrorId).HasColumnName("error_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(255)
                .HasColumnName("error_message");
            entity.Property(e => e.ErrorType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("unknown")
                .HasColumnName("error_type");
            entity.Property(e => e.Resolved)
                .HasDefaultValue(false)
                .HasColumnName("resolved");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Error_Rep__user___10566F31");
        });

        modelBuilder.Entity<EnrollmentUnlockExercise>(entity =>
        {

            entity.ToTable("Enrollment_Unlock_Exercise");
            entity.HasKey(e => e.EnrollmentUnlockExerciseId).HasName("PK__Enrollme__E81C034ED29BA84A");
            entity.Property(e => e.EnrollmentUnlockExerciseId).HasColumnName("enrollment_unlock_exercise_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.UnlockDate).HasColumnName("unlock_date");
            
            entity.HasOne(d => d.Enrollment).WithMany(p => p.EnrollmentUnlockExercises)
                .HasForeignKey(d => d.EnrollmentId)
                .HasConstraintName("FK__Enrollmen__enrol__4E1E9780");

            entity.HasOne(d => d.Exercise).WithMany(p => p.EnrollmentUnlockExercises)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK__Enrollmen__exerc__4F12BBB9");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418EC9CD6A75");

            entity.ToTable("Exercise");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(100)
                .HasColumnName("exercise_name");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsLocked).HasColumnName("is_locked");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__114A936A");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C76390A53");

            entity.ToTable("Exercise_Detail");

            entity.Property(e => e.ExerciseDetailId).HasColumnName("exercise_detail_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK__Exercise___exerc__123EB7A3");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__1332DBDC");
        });

        modelBuilder.Entity<ExerciseDetailResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailResultId).HasName("PK__Exercise__B2FE7229E398B6F6");

            entity.ToTable("Exercise_Detail_Result", tb =>
                {
                    tb.HasTrigger("trg_update_score_after_insert");
                    tb.HasTrigger("trg_update_score_after_update");
                });

            entity.Property(e => e.ExerciseDetailResultId).HasColumnName("exercise_detail_result_id");
            entity.Property(e => e.ExerciseDetailId).HasColumnName("exercise_detail_id");
            entity.Property(e => e.ExerciseResultId).HasColumnName("exercise_result_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("question_type");

            //entity.HasOne(d => d.ChoiceAnswer).WithMany(p => p.ExerciseDetailResults)
            //    .HasForeignKey(d => d.ChoiceAnswerId)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK__Exercise___choic__42ACE4D4");

            entity.HasOne(d => d.ExerciseDetail).WithMany(p => p.ExerciseDetailResults)
                .HasForeignKey(d => d.ExerciseDetailId)
                .HasConstraintName("FK__Exercise___exerc__14270015");

            entity.HasOne(d => d.ExerciseResult).WithMany(p => p.ExerciseDetailResults)
                .HasForeignKey(d => d.ExerciseResultId)
                .HasConstraintName("FK__Exercise___exerc__151B244E");
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0A326BA36C");

            entity.ToTable("Exercise_Result");

            entity.Property(e => e.ExerciseResultId).HasColumnName("exercise_result_id");
            entity.Property(e => e.DoneAt)
                .HasColumnType("datetime")
                .HasColumnName("done_at");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("score");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.ExerciseResults)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___enrol__160F4887");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__17036CC0");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__337243188733B59B");

            entity.ToTable("Fill_Answer");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(100)
                .HasColumnName("correct_answer");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.FillAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fill_Answ__quest__17F790F9");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BE42752177");

            entity.ToTable("Lesson", tb =>
                {
                    tb.HasTrigger("trg_after_delete_lesson");
                    tb.HasTrigger("trg_after_insert_lesson");
                });

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.LessonName)
                .HasMaxLength(100)
                .HasColumnName("lesson_name");
            entity.Property(e => e.LessonOrder).HasColumnName("lesson_order");
            entity.Property(e => e.LessonPdfUrl)
                .HasMaxLength(255)
                .HasColumnName("lesson_pdf_url");
            entity.Property(e => e.LessonVideoUrl)
                .HasMaxLength(255)
                .HasColumnName("lesson_video_url");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___18EBB532");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D6C7E8015");

            entity.ToTable("Lesson_Progress");

            entity.Property(e => e.LearningProgressId).HasColumnName("learning_progress_id");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.Process).HasColumnName("process");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("status"); ;

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__19DFD96B");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__1AD3FDA4");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__33724318358EB00B");

            entity.ToTable("Matching_Answer");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(100)
                .HasColumnName("correct_answer");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("img_url");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.MatchingAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Matching___quest__1BC821DD");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FBE2E9743");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.NotificationMessage)
                .HasMaxLength(255)
                .HasColumnName("notification_message");
            entity.Property(e => e.NotificationTitle)
                .HasMaxLength(255)
                .HasColumnName("notification_title");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("notification_type");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("unread")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__user___1CBC4616");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA104764AA");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.MethodId).HasColumnName("method_id");
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OrderID");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TransactionID");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__Payment__method___32767D0B");

            entity.HasOne(d => d.Plan).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Payment__plan_id__336AA144");

            entity.HasOne(d => d.Wallet).WithMany(p => p.Payments)
                .HasForeignKey(d => d.WalletId)
                .HasConstraintName("FK__Payment__wallet___251C81ED");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__PaymentM__747727B670B92BAF");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.MethodId).HasColumnName("method_id");
            entity.Property(e => e.MethodName)
                .HasMaxLength(255)
                .HasColumnName("method_name");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1DED1A80F2");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Coins).HasColumnName("coins");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .HasColumnName("plan_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC215491BC1368A");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("img_url");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.PdfSolution)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pdf_solution");
            entity.Property(e => e.QuestionContent)
                .HasMaxLength(255)
                .HasColumnName("question_content");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("question_type");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Question__lesson__208CD6FA");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02013B358C");

            entity.ToTable("Test");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.TestName)
                .HasMaxLength(100)
                .HasColumnName("test_name");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__2180FB33");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D232588CD064D0");

            entity.ToTable("Test_Detail");

            entity.Property(e => e.TestDetailId).HasColumnName("test_detail_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__22751F6C");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Test_Deta__test___236943A5");
        });

        modelBuilder.Entity<TestDetailResult>(entity =>
        {
            entity.HasKey(e => e.TestDetailResultId).HasName("PK__Test_Det__37921712DB4A9DEE");

            entity.ToTable("Test_Detail_Result");

            entity.Property(e => e.TestDetailResultId).HasColumnName("test_detail_result_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.TestDetailId).HasColumnName("test_detail_id");
            entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.TestDetailResults)
                .HasForeignKey(d => d.TestDetailId)
                .HasConstraintName("FK__Test_Deta__test___245D67DE");

            entity.HasOne(d => d.TestResult).WithMany(p => p.TestDetailResults)
                .HasForeignKey(d => d.TestResultId)
                .HasConstraintName("FK__Test_Deta__test___25518C17");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDA630B7218");

            entity.ToTable("Test_Result");

            entity.Property(e => e.TestResultId).HasColumnName("test_result_id");
            entity.Property(e => e.CompletionTime).HasColumnName("completion_time");
            entity.Property(e => e.DoneAt)
                .HasColumnType("datetime")
                .HasColumnName("done_at");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("score");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__enrol__2645B050");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___2739D489");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B1B0F65FB7");

            entity.ToTable("Token_Package");

            entity.Property(e => e.TokenPackageId).HasColumnName("token_package_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("package_name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Tokens).HasColumnName("tokens");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D59C57A3C7D");

            entity.ToTable("Token_Transaction");

            entity.Property(e => e.TokenTransactionId).HasColumnName("token_transaction_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.TokenAmount).HasColumnName("token_amount");
            entity.Property(e => e.TokenRemains).HasColumnName("token_remains");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");

            entity.HasOne(d => d.Wallet).WithMany(p => p.TokenTransactions)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Token_Tra__walle__3BFFE745");
        });

        modelBuilder.Entity<UserFillAnswer>(entity =>
        {
            entity.HasKey(e => e.UserFillAnswerId).HasName("PK__User_Fil__E5105288981F1E0A");

            entity.ToTable("User_Fill_Answer");

            entity.Property(e => e.UserFillAnswerId).HasColumnName("user_fill_answer_id");
            entity.Property(e => e.ExerciseDetailResultId).HasColumnName("exercise_detail_result_id");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.WrongAnswer)
                .HasMaxLength(100)
                .HasColumnName("wrong_answer");

            entity.HasOne(d => d.ExerciseDetailResult).WithMany(p => p.UserFillAnswers)
                .HasForeignKey(d => d.ExerciseDetailResultId)
                .HasConstraintName("FK__User_Fill__exerc__41B8C09B");
        });

        modelBuilder.Entity<UserChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.UserChoiceAnswerId).HasName("PK__User_Cho__31EEBA91B2F2A794");
            entity.ToTable("User_Choice_Answer");
            entity.Property(e => e.UserChoiceAnswerId).HasColumnName("user_choice_answer_id");
            entity.Property(e => e.ExerciseDetailResultId).HasColumnName("exercise_detail_result_id");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

            entity.HasOne(d => d.ExerciseDetailResult).WithMany(p => p.UserChoiceAnswers)
                .HasForeignKey(d => d.ExerciseDetailResultId)
                .HasConstraintName("FK__User_Choi__exerc__4959E263");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.UserSessionId).HasName("PK__User_Ses__DE7C9ED61087F440");

            entity.ToTable("User_Session");

            entity.Property(e => e.UserSessionId).HasColumnName("user_session_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("login_time");
            entity.Property(e => e.LogoutTime).HasColumnName("logout_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__User_Session__user_id__4F7CD00D");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__0EE6F0414E6E92A0");

            entity.ToTable("Wallet");

            entity.Property(e => e.WalletId).HasColumnName("wallet_id");
            entity.Property(e => e.CoinRemains).HasColumnName("coin_remains");
            entity.Property(e => e.TokenRemains).HasColumnName("token_remains");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Wallet__user_id__24285DB4");
        });

        base.OnModelCreating(modelBuilder);
    }


}
