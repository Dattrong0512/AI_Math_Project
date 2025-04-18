using AIMathProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AIMathProject.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }


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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("AspNetUsers");       

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__745EFE872B223990");

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
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__FD040B17113CE11E");

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
                .HasConstraintName("FK__Chat__support_ag__32AB8735");

            entity.HasOne(d => d.User).WithMany(p => p.ChatUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Chat__user_id__31B762FC");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Chat_Mes__0BBF6EE6ED8C769F");

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
                .HasConstraintName("FK__Chat_Mess__chat___367C1819");
        });

        modelBuilder.Entity<ChoiceAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Choice_A__337243182E7DD80B");

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
                .HasConstraintName("FK__Choice_An__quest__03F0984C");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E79576871A9F4F74");

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
                .HasConstraintName("FK__Comment__lesson___2DE6D218");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comment__parent___2BFE89A6");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__user_id__2CF2ADDF");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7A57661DB0");

            entity.ToTable("Enrollment");

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
                .HasConstraintName("FK__Enrollmen__user___6FE99F9F");

            entity.ToTable(tb =>
            {
                tb.HasTrigger("trg_create_lesson_progress");
            });
        });

        modelBuilder.Entity<ErrorReport>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__Error_Re__DA71E16CBE836E35");

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
                .HasConstraintName("FK__Error_Rep__user___41EDCAC5");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418E1118BA51");

            entity.ToTable("Exercise");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(100)
                .HasColumnName("exercise_name");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise__lesson__114A936A");
        });

        modelBuilder.Entity<ExerciseDetail>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailId).HasName("PK__Exercise__CF31D69C1DFCFEF2");

            entity.ToTable("Exercise_Detail");

            entity.Property(e => e.ExerciseDetailId).HasColumnName("exercise_detail_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseDetails)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK__Exercise___exerc__14270015");

            entity.HasOne(d => d.Question).WithMany(p => p.ExerciseDetails)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___quest__151B244E");
        });

        modelBuilder.Entity<ExerciseDetailResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseDetailResultId).HasName("PK__Exercise__B2FE72291D9EB615");

            entity.ToTable("Exercise_Detail_Result");

            entity.Property(e => e.ExerciseDetailResultId).HasColumnName("exercise_detail_result_id");
            entity.Property(e => e.ExerciseDetailId).HasColumnName("exercise_detail_id");
            entity.Property(e => e.ExerciseResultId).HasColumnName("exercise_result_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

            entity.HasOne(d => d.ExerciseDetail).WithMany(p => p.ExerciseDetailResults)
                .HasForeignKey(d => d.ExerciseDetailId)
                .HasConstraintName("FK__Exercise___exerc__236943A5");

            entity.HasOne(d => d.ExerciseResult).WithMany(p => p.ExerciseDetailResults)
                .HasForeignKey(d => d.ExerciseResultId)
                .HasConstraintName("FK__Exercise___exerc__245D67DE");

            entity.ToTable(tb =>
            {
                tb.HasTrigger("trg_update_score_after_insert");
                tb.HasTrigger("trg_update_score_after_update");
            });
        });

        modelBuilder.Entity<ExerciseResult>(entity =>
        {
            entity.HasKey(e => e.ExerciseResultId).HasName("PK__Exercise__AD9A3B0A3C6A7837");

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
                .HasConstraintName("FK__Exercise___enrol__208CD6FA");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseResults)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Exercise___exerc__1F98B2C1");
        });

        modelBuilder.Entity<FillAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Fill_Ans__3372431814241484");

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
                .HasConstraintName("FK__Fill_Answ__quest__0A9D95DB");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BE8F73636D");

            entity.ToTable("Lesson");

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.LessonVideoUrl)
                .HasMaxLength(255)
                .HasColumnName("lesson_video_url");
            entity.Property(e => e.LessonPdfUrl)
                .HasMaxLength(255)
                .HasColumnName("lesson_pdf_url");
            entity.Property(e => e.LessonName)
                .HasMaxLength(100)
                .HasColumnName("lesson_name");
            entity.Property(e => e.LessonOrder).HasColumnName("lesson_order");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson__chapter___787EE5A0");

            entity.ToTable(tb =>
            {
                tb.HasTrigger("trg_after_insert_lesson");
                tb.HasTrigger("trg_after_delete_lesson");
            });
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.LearningProgressId).HasName("PK__Lesson_P__5BA9A40D0C38A3C3");

            entity.ToTable("Lesson_Progress");

            entity.Property(e => e.LearningProgressId).HasColumnName("learning_progress_id");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__enrol__282DF8C2");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lesson_Pr__lesso__2739D489");
        });

        modelBuilder.Entity<MatchingAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Matching__337243182B53B279");

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
                .HasConstraintName("FK__Matching___quest__07C12930");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FBF0CEF9F");

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
                .HasConstraintName("FK__Notificat__user___3B40CD36");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA63D5053C");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.PaymentName)
                .HasMaxLength(100)
                .HasColumnName("payment_name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__BE9F8F1D256113F1");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .HasColumnName("plan_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<PlanTransaction>(entity =>
        {
            entity.HasKey(e => e.PlanTransactionId).HasName("PK__Plan_Tra__6A8B2E59818D54EC");

            entity.ToTable("Plan_Transaction");

            entity.Property(e => e.PlanTransactionId).HasColumnName("plan_transaction_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Payment).WithMany(p => p.PlanTransactions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__payme__6C190EBB");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanTransactions)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__plan___6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.PlanTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Plan_Tran__user___6A30C649");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549ED70DA03");

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
                .HasConstraintName("FK__Question__lesson__00200768");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__F3FF1C02CBA0D51C");

            entity.ToTable("Test");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.TestName)
                .HasMaxLength(100)
                .HasColumnName("test_name");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test__chapter_id__7B5B524B");
        });

        modelBuilder.Entity<TestDetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__Test_Det__B8D23258609AEC20");

            entity.ToTable("Test_Detail");

            entity.Property(e => e.TestDetailId).HasColumnName("test_detail_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TestDetails)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Deta__quest__0E6E26BF");

            entity.HasOne(d => d.Test).WithMany(p => p.TestDetails)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Test_Deta__test___0D7A0286");
        });

        modelBuilder.Entity<TestDetailResult>(entity =>
        {
            entity.HasKey(e => e.TestDetailResultId).HasName("PK__Test_Det__37921712DC895AE5");

            entity.ToTable("Test_Detail_Result");

            entity.Property(e => e.TestDetailResultId).HasColumnName("test_detail_result_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.TestDetailId).HasColumnName("test_detail_id");
            entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.TestDetailResults)
                .HasForeignKey(d => d.TestDetailId)
                .HasConstraintName("FK__Test_Deta__test___1BC821DD");

            entity.HasOne(d => d.TestResult).WithMany(p => p.TestDetailResults)
                .HasForeignKey(d => d.TestResultId)
                .HasConstraintName("FK__Test_Deta__test___1CBC4616");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__Test_Res__152BCEDA77BF0C91");

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
                .HasConstraintName("FK__Test_Resu__enrol__18EBB532");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Test_Resu__test___17F790F9");
        });

        modelBuilder.Entity<TokenPackage>(entity =>
        {
            entity.HasKey(e => e.TokenPackageId).HasName("PK__Token_Pa__F49422B17643D6F4");

            entity.ToTable("Token_Package");

            entity.Property(e => e.TokenPackageId).HasColumnName("token_package_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("package_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Tokens).HasColumnName("tokens");
        });

        modelBuilder.Entity<TokenTransaction>(entity =>
        {
            entity.HasKey(e => e.TokenTransactionId).HasName("PK__Token_Tr__A3DC9D59D2E60AED");

            entity.ToTable("Token_Transaction");

            entity.Property(e => e.TokenTransactionId).HasColumnName("token_transaction_id");
            entity.Property(e => e.Change).HasColumnName("change");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.TokenPackageId).HasColumnName("token_package_id");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Payment).WithMany(p => p.TokenTransactions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__payme__6383C8BA");

            entity.HasOne(d => d.TokenPackage).WithMany(p => p.TokenTransactions)
                .HasForeignKey(d => d.TokenPackageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__token__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.TokenTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Token_Tra__user___619B8048");
        });

        base.OnModelCreating(modelBuilder);
        
    }

   
}
