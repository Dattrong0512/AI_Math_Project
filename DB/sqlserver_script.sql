USE AI_MATH;

DROP TABLE IF EXISTS Error_Report;
DROP TABLE IF EXISTS Notifications;
DROP TABLE IF EXISTS Chat_Message;
DROP TABLE IF EXISTS Chat;
DROP TABLE IF EXISTS Comment;
DROP TABLE IF EXISTS Lesson_Progress;
DROP TABLE IF EXISTS Exercise_Result;
DROP TABLE IF EXISTS Test_Result;
DROP TABLE IF EXISTS Exercise_Detail;
DROP TABLE IF EXISTS Test_Detail;
DROP TABLE IF EXISTS Choice_Answer;
DROP TABLE IF EXISTS Matching_Answer;
DROP TABLE IF EXISTS Fill_Answer;
DROP TABLE IF EXISTS Exercise;
DROP TABLE IF EXISTS Test;
DROP TABLE IF EXISTS Question;
DROP TABLE IF EXISTS Lesson;
DROP TABLE IF EXISTS Chapter;
DROP TABLE IF EXISTS Enrollment;
DROP TABLE IF EXISTS Plan_Transaction;
DROP TABLE IF EXISTS Plans;
DROP TABLE IF EXISTS Token_Transaction;
DROP TABLE IF EXISTS Token_Package;
DROP TABLE IF EXISTS Payment;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Administrators;

CREATE TABLE Administrators (
    admin_id INT IDENTITY(1,1) PRIMARY KEY,
    admin_name NVARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    admin_password VARCHAR(255) NOT NULL,
    gender VARCHAR(10) CHECK (gender IN ('Male', 'Female', 'Other')),
    dob DATE,
    avatar TEXT,
    status BIT DEFAULT 1
);

CREATE TABLE Payment (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    payment_name NVARCHAR(100) NOT NULL,
    description TEXT,
    status BIT DEFAULT 1
);

CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    user_name NVARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    user_password VARCHAR(255) NOT NULL,
    gender VARCHAR(10) CHECK (gender IN ('Male', 'Female', 'Other')),
    dob DATE,
    avatar TEXT,
    status BIT DEFAULT 1
);

CREATE TABLE Token_Package (
    token_package_id INT IDENTITY(1,1) PRIMARY KEY,
    package_name NVARCHAR(100) NOT NULL,
    tokens INT,
    price DECIMAL(10,2),
    description NVARCHAR(255),
);

CREATE TABLE Token_Transaction (
    token_transaction_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    token_package_id INT FOREIGN KEY REFERENCES Token_Package(token_package_id) ON DELETE CASCADE,
    payment_id INT FOREIGN KEY REFERENCES Payment(payment_id) ON DELETE CASCADE,
    change INT,
    transaction_type VARCHAR(5) CHECK (transaction_type IN ('in', 'out')),
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Plans (
    plan_id INT IDENTITY(1,1) PRIMARY KEY,
    plan_name NVARCHAR(100) NOT NULL,
    price DECIMAL(10,2),
    duration_days INT,
    description NVARCHAR(255)
);

CREATE TABLE Plan_Transaction (
    plan_transaction_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    plan_id INT FOREIGN KEY REFERENCES Plans(plan_id) ON DELETE CASCADE,
    payment_id INT FOREIGN KEY REFERENCES Payment(payment_id) ON DELETE CASCADE,
    created_at DATETIME DEFAULT GETDATE(),
    expires_at DATETIME
);

CREATE TABLE Enrollment (
    enrollment_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    grade SMALLINT CHECK (grade IN (1, 2, 3, 4, 5)),
    enrollment_date DATE,
    avg_score DECIMAL(4,2),
    semester SMALLINT CHECK (semester IN (1, 2, 3)),
    start_year SMALLINT,
    end_year SMALLINT
);

CREATE TABLE Chapter (
    chapter_id INT IDENTITY(1,1) PRIMARY KEY,
	chapter_order SMALLINT,
    chapter_name NVARCHAR(100) NOT NULL,
	grade SMALLINT CHECK (grade IN (1, 2, 3, 4, 5)),
    description TEXT
);

CREATE TABLE Lesson (
    lesson_id INT IDENTITY(1,1) PRIMARY KEY,
	lesson_order SMALLINT,
    lesson_name NVARCHAR(100) NOT NULL,
    chapter_id INT FOREIGN KEY REFERENCES Chapter(chapter_id) ON DELETE CASCADE,
    lesson_content NVARCHAR(255)
);

CREATE TABLE Test (
    test_id INT IDENTITY(1,1) PRIMARY KEY,
    chapter_id INT FOREIGN KEY REFERENCES Chapter(chapter_id) ON DELETE CASCADE,
    test_name NVARCHAR(100)
);

CREATE TABLE Question (
    question_id INT IDENTITY(1,1) PRIMARY KEY,
    question_type VARCHAR(20) CHECK (question_type IN ('multiple_choice','fill_in_blank','matching')),
	difficulty SMALLINT CHECK (difficulty IN (1,2,3,4)),
    lesson_id INT FOREIGN KEY REFERENCES Lesson(lesson_id) ON DELETE CASCADE,
    img_url VARCHAR(100) DEFAULT NULL,
    question_content NVARCHAR(255),
	pdf_solution VARCHAR(100)
);

CREATE TABLE Choice_Answer (
	answer_id INT IDENTITY(1,1) PRIMARY KEY,
	question_id INT FOREIGN KEY REFERENCES Question(question_id) ON DELETE CASCADE,
	content NVARCHAR(100),
	is_correct BIT,
	img_url VARCHAR(100) DEFAULT NULL,
);

CREATE TABLE Matching_Answer (
	answer_id INT IDENTITY(1,1) PRIMARY KEY,
	question_id INT FOREIGN KEY REFERENCES Question(question_id)ON DELETE CASCADE,
	correct_answer NVARCHAR(100) NOT NULL,
	img_url VARCHAR(100) NOT NULL
);

CREATE TABLE Fill_Answer (
	answer_id INT IDENTITY(1,1) PRIMARY KEY,
	question_id INT FOREIGN KEY REFERENCES Question(question_id) ON DELETE CASCADE,
	correct_answer NVARCHAR(100) NOT NULL,
	position SMALLINT NOT NULL
)

CREATE TABLE Test_Detail (
    test_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    test_id INT FOREIGN KEY REFERENCES Test(test_id),
    question_id INT FOREIGN KEY REFERENCES Question(question_id) ON DELETE CASCADE,
);

CREATE TABLE Exercise (
    exercise_id INT IDENTITY(1,1) PRIMARY KEY,
    exercise_name NVARCHAR(100) NOT NULL,
    lesson_id INT FOREIGN KEY REFERENCES Lesson(lesson_id) ON DELETE CASCADE,
);

CREATE TABLE Exercise_Detail (
    exercise_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    exercise_id INT FOREIGN KEY REFERENCES Exercise(exercise_id),
    question_id INT FOREIGN KEY REFERENCES Question(question_id) ON DELETE CASCADE,
);

CREATE TABLE Test_Result (
    test_result_id INT IDENTITY(1,1) PRIMARY KEY,
    test_id INT FOREIGN KEY REFERENCES Test(test_id) ON DELETE CASCADE,
    enrollment_id INT FOREIGN KEY REFERENCES Enrollment(enrollment_id) ON DELETE CASCADE,
    score DECIMAL(4,2),
    completion_time SMALLINT,
    done_at DATETIME
);

CREATE TABLE Exercise_Result (
    exercise_result_id INT IDENTITY(1,1) PRIMARY KEY,
    exercise_id INT FOREIGN KEY REFERENCES Exercise(exercise_id) ON DELETE CASCADE,
    enrollment_id INT FOREIGN KEY REFERENCES Enrollment(enrollment_id) ON DELETE CASCADE,
    score DECIMAL(4,2),
    done_at DATETIME
);

CREATE TABLE Lesson_Progress (
    learning_progress_id INT IDENTITY(1,1) PRIMARY KEY,
    lesson_id INT FOREIGN KEY REFERENCES Lesson(lesson_id) ON DELETE CASCADE,
    enrollment_id INT FOREIGN KEY REFERENCES Enrollment(enrollment_id) ON DELETE CASCADE,
    learning_progress SMALLINT,
    is_completed BIT DEFAULT 0
);

CREATE TABLE Comment (
    comment_id INT IDENTITY(1,1) PRIMARY KEY,
    parent_comment_id INT FOREIGN KEY REFERENCES Comment(comment_id),
    user_id INT FOREIGN KEY REFERENCES Users(user_id),
    lesson_id INT FOREIGN KEY REFERENCES Lesson(lesson_id),
    comment_content NVARCHAR(300),
    status VARCHAR(100),
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Chat (
    chat_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    support_agent_id INT,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Chat_Message (
    message_id INT IDENTITY(1,1) PRIMARY KEY,
    chat_id INT FOREIGN KEY REFERENCES Chat(chat_id) ON DELETE CASCADE,
    message_direction VARCHAR(10) CHECK (message_direction IN ('user', 'support')), 
    message_content NVARCHAR(255),
    sent_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE Notifications (
    notification_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    notification_type VARCHAR(50) CHECK (notification_type IN ('success', 'error', 'warning', 'info')),
    notification_title VARCHAR(255),
    notification_message NVARCHAR(255),
    sent_at DATETIME DEFAULT GETDATE(),
    status VARCHAR(20) CHECK (status IN ('unread', 'read')) DEFAULT 'unread'
);

CREATE TABLE Error_Report (
    report_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) ON DELETE CASCADE,
    error_message NVARCHAR(255),
    error_type VARCHAR(100) DEFAULT 'unknown', 
    created_at DATETIME DEFAULT GETDATE(),
    resolved BIT DEFAULT 0 
);

DBCC CHECKIDENT ('Error_Report', RESEED, 1);
DBCC CHECKIDENT ('Notifications', RESEED, 1);
DBCC CHECKIDENT ('Chat_Message', RESEED, 1);
DBCC CHECKIDENT ('Chat', RESEED, 1);
DBCC CHECKIDENT ('Comment', RESEED, 1);
DBCC CHECKIDENT ('Lesson_Progress', RESEED, 1);
DBCC CHECKIDENT ('Exercise_Result', RESEED, 1);
DBCC CHECKIDENT ('Test_Result', RESEED, 1);
DBCC CHECKIDENT ('Exercise_Detail', RESEED, 1);
DBCC CHECKIDENT ('Exercise', RESEED, 1);
DBCC CHECKIDENT ('Test_Detail', RESEED, 1);
DBCC CHECKIDENT ('Question', RESEED, 1);
DBCC CHECKIDENT ('Test', RESEED, 1);
DBCC CHECKIDENT ('Lesson', RESEED, 1);
DBCC CHECKIDENT ('Chapter', RESEED, 1);
DBCC CHECKIDENT ('Enrollment', RESEED, 1);
DBCC CHECKIDENT ('Plan_Transaction', RESEED, 1);
DBCC CHECKIDENT ('Plans', RESEED, 1);
DBCC CHECKIDENT ('Token_Transaction', RESEED, 1);
DBCC CHECKIDENT ('Token_Package', RESEED, 1);
DBCC CHECKIDENT ('Payment', RESEED, 1);
DBCC CHECKIDENT ('Users', RESEED, 1);
DBCC CHECKIDENT ('Administrators', RESEED, 1);
