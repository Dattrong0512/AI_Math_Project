using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIMathProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    chapter_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapter_order = table.Column<short>(type: "smallint", nullable: true),
                    chapter_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    grade = table.Column<short>(type: "smallint", nullable: true),
                    semester = table.Column<short>(type: "smallint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chapter__745EFE872B223990", x => x.chapter_id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__ED1FC9EA63D5053C", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    plan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plan_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    duration_days = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plans__BE9F8F1D256113F1", x => x.plan_id);
                });

            migrationBuilder.CreateTable(
                name: "Token_Package",
                columns: table => new
                {
                    token_package_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    package_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tokens = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Token_Pa__F49422B17643D6F4", x => x.token_package_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Session",
                columns: table => new
                {
                    user_session_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    login_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    logout_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    duration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Session__45F365D3", x => x.user_session_id);
                    table.ForeignKey(
                        name: "FK_User_Session_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    chat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    support_agent_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chat__FD040B17113CE11E", x => x.chat_id);
                    table.ForeignKey(
                        name: "FK__Chat__support_ag__32AB8735",
                        column: x => x.support_agent_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Chat__user_id__31B762FC",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    grade = table.Column<short>(type: "smallint", nullable: true),
                    enrollment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    avg_score = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    semester = table.Column<short>(type: "smallint", nullable: true),
                    start_year = table.Column<short>(type: "smallint", nullable: true),
                    end_year = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__6D24AA7A57661DB0", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK__Enrollmen__user___6FE99F9F",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Error_Report",
                columns: table => new
                {
                    error_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    error_message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    error_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "unknown"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    resolved = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Error_Re__DA71E16CBE836E35", x => x.error_id);
                    table.ForeignKey(
                        name: "FK__Error_Rep__user___41EDCAC5",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    notification_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    notification_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    notification_message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValue: "unread")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__E059842FBF0CEF9F", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK__Notificat__user___3B40CD36",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    lesson_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lesson_order = table.Column<short>(type: "smallint", nullable: true),
                    lesson_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    chapter_id = table.Column<int>(type: "int", nullable: true),
                    lesson_video_url = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    lesson_pdf_url = table.Column<string>(type: "nvarchar(255)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lesson__6421F7BE8F73636D", x => x.lesson_id);
                    table.ForeignKey(
                        name: "FK__Lesson__chapter___787EE5A0",
                        column: x => x.chapter_id,
                        principalTable: "Chapter",
                        principalColumn: "chapter_id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    test_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chapter_id = table.Column<int>(type: "int", nullable: true),
                    test_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test__F3FF1C02CBA0D51C", x => x.test_id);
                    table.ForeignKey(
                        name: "FK__Test__chapter_id__7B5B524B",
                        column: x => x.chapter_id,
                        principalTable: "Chapter",
                        principalColumn: "chapter_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan_Transaction",
                columns: table => new
                {
                    plan_transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    plan_id = table.Column<int>(type: "int", nullable: true),
                    payment_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    expires_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plan_Tra__6A8B2E59818D54EC", x => x.plan_transaction_id);
                    table.ForeignKey(
                        name: "FK__Plan_Tran__payme__6C190EBB",
                        column: x => x.payment_id,
                        principalTable: "Payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Plan_Tran__plan___6B24EA82",
                        column: x => x.plan_id,
                        principalTable: "Plans",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Plan_Tran__user___6A30C649",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Token_Transaction",
                columns: table => new
                {
                    token_transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    token_package_id = table.Column<int>(type: "int", nullable: true),
                    payment_id = table.Column<int>(type: "int", nullable: true),
                    change = table.Column<int>(type: "int", nullable: true),
                    transaction_type = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Token_Tr__A3DC9D59D2E60AED", x => x.token_transaction_id);
                    table.ForeignKey(
                        name: "FK__Token_Tra__payme__6383C8BA",
                        column: x => x.payment_id,
                        principalTable: "Payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Token_Tra__token__628FA481",
                        column: x => x.token_package_id,
                        principalTable: "Token_Package",
                        principalColumn: "token_package_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Token_Tra__user___619B8048",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat_Message",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chat_id = table.Column<int>(type: "int", nullable: true),
                    message_direction = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    message_content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chat_Mes__0BBF6EE6ED8C769F", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__Chat_Mess__chat___367C1819",
                        column: x => x.chat_id,
                        principalTable: "Chat",
                        principalColumn: "chat_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_comment_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    lesson_id = table.Column<int>(type: "int", nullable: true),
                    comment_content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__E79576871A9F4F74", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK__Comment__lesson___2DE6D218",
                        column: x => x.lesson_id,
                        principalTable: "Lesson",
                        principalColumn: "lesson_id");
                    table.ForeignKey(
                        name: "FK__Comment__parent___2BFE89A6",
                        column: x => x.parent_comment_id,
                        principalTable: "Comment",
                        principalColumn: "comment_id");
                    table.ForeignKey(
                        name: "FK__Comment__user_id__2CF2ADDF",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    exercise_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exercise_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lesson_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__C121418E1118BA51", x => x.exercise_id);
                    table.ForeignKey(
                        name: "FK__Exercise__lesson__114A936A",
                        column: x => x.lesson_id,
                        principalTable: "Lesson",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson_Progress",
                columns: table => new
                {
                    learning_progress_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lesson_id = table.Column<int>(type: "int", nullable: true),
                    enrollment_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lesson_P__5BA9A40D0C38A3C3", x => x.learning_progress_id);
                    table.ForeignKey(
                        name: "FK__Lesson_Pr__enrol__282DF8C2",
                        column: x => x.enrollment_id,
                        principalTable: "Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Lesson_Pr__lesso__2739D489",
                        column: x => x.lesson_id,
                        principalTable: "Lesson",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    difficulty = table.Column<short>(type: "smallint", nullable: true),
                    lesson_id = table.Column<int>(type: "int", nullable: true),
                    img_url = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true, defaultValueSql: "(NULL)"),
                    question_content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pdf_solution = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__2EC21549ED70DA03", x => x.question_id);
                    table.ForeignKey(
                        name: "FK__Question__lesson__00200768",
                        column: x => x.lesson_id,
                        principalTable: "Lesson",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test_Result",
                columns: table => new
                {
                    test_result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_id = table.Column<int>(type: "int", nullable: true),
                    enrollment_id = table.Column<int>(type: "int", nullable: true),
                    score = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    completion_time = table.Column<short>(type: "smallint", nullable: true),
                    done_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Res__152BCEDA77BF0C91", x => x.test_result_id);
                    table.ForeignKey(
                        name: "FK__Test_Resu__enrol__18EBB532",
                        column: x => x.enrollment_id,
                        principalTable: "Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Test_Resu__test___17F790F9",
                        column: x => x.test_id,
                        principalTable: "Test",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Result",
                columns: table => new
                {
                    exercise_result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exercise_id = table.Column<int>(type: "int", nullable: true),
                    enrollment_id = table.Column<int>(type: "int", nullable: true),
                    score = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    done_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__AD9A3B0A3C6A7837", x => x.exercise_result_id);
                    table.ForeignKey(
                        name: "FK__Exercise___enrol__208CD6FA",
                        column: x => x.enrollment_id,
                        principalTable: "Enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Exercise___exerc__1F98B2C1",
                        column: x => x.exercise_id,
                        principalTable: "Exercise",
                        principalColumn: "exercise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choice_Answer",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_correct = table.Column<bool>(type: "bit", nullable: true),
                    img_url = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Choice_A__337243182E7DD80B", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK__Choice_An__quest__03F0984C",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Detail",
                columns: table => new
                {
                    exercise_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exercise_id = table.Column<int>(type: "int", nullable: true),
                    question_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__CF31D69C1DFCFEF2", x => x.exercise_detail_id);
                    table.ForeignKey(
                        name: "FK__Exercise___exerc__14270015",
                        column: x => x.exercise_id,
                        principalTable: "Exercise",
                        principalColumn: "exercise_id");
                    table.ForeignKey(
                        name: "FK__Exercise___quest__151B244E",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fill_Answer",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: true),
                    correct_answer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    position = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fill_Ans__3372431814241484", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK__Fill_Answ__quest__0A9D95DB",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matching_Answer",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: true),
                    correct_answer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    img_url = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Matching__337243182B53B279", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK__Matching___quest__07C12930",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test_Detail",
                columns: table => new
                {
                    test_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_id = table.Column<int>(type: "int", nullable: true),
                    question_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Det__B8D23258609AEC20", x => x.test_detail_id);
                    table.ForeignKey(
                        name: "FK__Test_Deta__quest__0E6E26BF",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Test_Deta__test___0D7A0286",
                        column: x => x.test_id,
                        principalTable: "Test",
                        principalColumn: "test_id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Detail_Result",
                columns: table => new
                {
                    exercise_detail_result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exercise_detail_id = table.Column<int>(type: "int", nullable: true),
                    exercise_result_id = table.Column<int>(type: "int", nullable: true),
                    is_correct = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__B2FE72291D9EB615", x => x.exercise_detail_result_id);
                    table.ForeignKey(
                        name: "FK__Exercise___exerc__236943A5",
                        column: x => x.exercise_detail_id,
                        principalTable: "Exercise_Detail",
                        principalColumn: "exercise_detail_id");
                    table.ForeignKey(
                        name: "FK__Exercise___exerc__245D67DE",
                        column: x => x.exercise_result_id,
                        principalTable: "Exercise_Result",
                        principalColumn: "exercise_result_id");
                });

            migrationBuilder.CreateTable(
                name: "Test_Detail_Result",
                columns: table => new
                {
                    test_detail_result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_detail_id = table.Column<int>(type: "int", nullable: true),
                    test_result_id = table.Column<int>(type: "int", nullable: true),
                    is_correct = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Det__37921712DC895AE5", x => x.test_detail_result_id);
                    table.ForeignKey(
                        name: "FK__Test_Deta__test___1BC821DD",
                        column: x => x.test_detail_id,
                        principalTable: "Test_Detail",
                        principalColumn: "test_detail_id");
                    table.ForeignKey(
                        name: "FK__Test_Deta__test___1CBC4616",
                        column: x => x.test_result_id,
                        principalTable: "Test_Result",
                        principalColumn: "test_result_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_support_agent_id",
                table: "Chat",
                column: "support_agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_user_id",
                table: "Chat",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Session_user_id",
                table: "User_Session",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_Message_chat_id",
                table: "Chat_Message",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_Answer_question_id",
                table: "Choice_Answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_lesson_id",
                table: "Comment",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_parent_comment_id",
                table: "Comment",
                column: "parent_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_user_id",
                table: "Comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_user_id",
                table: "Enrollment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Error_Report_user_id",
                table: "Error_Report",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_lesson_id",
                table: "Exercise",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Detail_exercise_id",
                table: "Exercise_Detail",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Detail_question_id",
                table: "Exercise_Detail",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Detail_Result_exercise_detail_id",
                table: "Exercise_Detail_Result",
                column: "exercise_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Detail_Result_exercise_result_id",
                table: "Exercise_Detail_Result",
                column: "exercise_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Result_enrollment_id",
                table: "Exercise_Result",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Result_exercise_id",
                table: "Exercise_Result",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fill_Answer_question_id",
                table: "Fill_Answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_chapter_id",
                table: "Lesson",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_Progress_enrollment_id",
                table: "Lesson_Progress",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_Progress_lesson_id",
                table: "Lesson_Progress",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_Matching_Answer_question_id",
                table: "Matching_Answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_user_id",
                table: "Notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Transaction_payment_id",
                table: "Plan_Transaction",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Transaction_plan_id",
                table: "Plan_Transaction",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Transaction_user_id",
                table: "Plan_Transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Question_lesson_id",
                table: "Question",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_chapter_id",
                table: "Test",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Detail_question_id",
                table: "Test_Detail",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Detail_test_id",
                table: "Test_Detail",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Detail_Result_test_detail_id",
                table: "Test_Detail_Result",
                column: "test_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Detail_Result_test_result_id",
                table: "Test_Detail_Result",
                column: "test_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Result_enrollment_id",
                table: "Test_Result",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Result_test_id",
                table: "Test_Result",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_Token_Transaction_payment_id",
                table: "Token_Transaction",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Token_Transaction_token_package_id",
                table: "Token_Transaction",
                column: "token_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Token_Transaction_user_id",
                table: "Token_Transaction",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Chat_Message");

            migrationBuilder.DropTable(
                name: "Choice_Answer");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Error_Report");

            migrationBuilder.DropTable(
                name: "Exercise_Detail_Result");

            migrationBuilder.DropTable(
                name: "Fill_Answer");

            migrationBuilder.DropTable(
                name: "Lesson_Progress");

            migrationBuilder.DropTable(
                name: "Matching_Answer");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Plan_Transaction");

            migrationBuilder.DropTable(
                name: "Test_Detail_Result");

            migrationBuilder.DropTable(
                name: "Token_Transaction");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Exercise_Detail");

            migrationBuilder.DropTable(
                name: "Exercise_Result");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Test_Detail");

            migrationBuilder.DropTable(
                name: "Test_Result");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Token_Package");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "User_Session");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Chapter");
        }
    }
}
