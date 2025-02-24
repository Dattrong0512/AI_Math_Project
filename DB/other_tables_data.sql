INSERT INTO Administrators (admin_name, email, admin_password, gender, dob, avatar, status) 
VALUES 
(N'Nguyễn Văn An', N'an.nguyen231@gmail.com', 'hashed_password_1', 'Male', '1985-07-15', NULL, 1),
(N'Trần Thị Bích', N'bich.tran391@yahoo.com', 'hashed_password_2', 'Female', '1990-09-22', NULL, 1),
(N'Lê Quốc Đạt', N'dat.le893@gmail.com', 'hashed_password_3', 'Male', '1988-05-10', NULL, 1),
(N'Phạm Thanh Hòa', N'hoa.pham472@yahoo.com', 'hashed_password_4', 'Other', '1995-11-30', NULL, 1),
(N'Hoàng Minh Khang', N'khang.hoang006@gmail.com', 'hashed_password_5', 'Male', '1982-03-25', NULL, 0);

INSERT INTO Users (user_name, email, user_password, gender, dob, avatar, status) 
VALUES 
(N'Nguyễn Minh Khang', N'khangnguyen_2015@gmail.com', 'hashed_password_1', 'Male', '2015-08-14', NULL, 1),
(N'Trần Gia Hân', N'giahan_tran@yahoo.com', 'hashed_password_2', 'Female', '2014-05-23', NULL, 1),
(N'Phạm Quốc Bảo', N'bao_pham2016@gmail.com', 'hashed_password_3', 'Male', '2016-12-02', NULL, 1),
(N'Lê Hoàng Minh', N'hoangminh_le@yahoo.com', 'hashed_password_4', 'Male', '2013-09-17', NULL, 1),
(N'Đặng Thị Hương', N'huongdang_2017@gmail.com', 'hashed_password_5', 'Female', '2017-04-05', NULL, 0),
(N'Hoàng Đức Tín', N'ductin_hoang01@gmail.com', 'hashed_password_6', 'Male', '2015-06-30', NULL, 1),
(N'Vũ Ngọc Linh', N'vungoclinh_14@yahoo.com', 'hashed_password_7', 'Female', '2014-11-19', NULL, 1),
(N'Bùi Thành Công', N'congbui_2013@gmail.com', 'hashed_password_8', 'Male', '2013-02-28', NULL, 1),
(N'Ngô Thu Hà', N'thuha.ngo03@yahoo.com', 'hashed_password_9', 'Female', '2018-03-21', NULL, 0),
(N'Đoàn Thanh Sơn', N'son.doan2016@gmail.com', 'hashed_password_10', 'Male', '2016-07-12', NULL, 1);

INSERT INTO Enrollment (user_id, grade, enrollment_date, avg_score, semester, start_year, end_year)
VALUES
(6, 3, '2023-08-12', 8.3, 1, 2023, 2024),
(6, 3, '2024-01-12', 8.6, 2, 2023, 2024),
(6, 4, '2024-08-14', NULL, 1, 2024, 2025),
(1, 3, '2024-08-15', NULL, 1, 2024, 2025),
(2, 4, '2024-08-12', NULL, 1, 2024, 2025),
(3, 2, '2024-08-10', NULL, 1, 2024, 2025),
(4, 5, '2024-08-18', NULL, 1, 2024, 2025),
(5, 1, '2024-08-20', NULL, 1, 2024, 2025),
(7, 5, '2024-08-17', NULL, 1, 2024, 2025),
(8, 5, '2024-08-19', NULL, 1, 2024, 2025),
(9, 1, '2024-08-21', NULL, 1, 2024, 2025),
(10, 2, '2024-08-22', NULL, 1, 2024, 2025);

INSERT INTO Exercise (exercise_name, lesson_id) 
VALUES
-- Lớp 1
(N'Bài tập Vị trí', 1),
(N'Bài tập Khối hộp chữ nhật - Khối lập phương', 2),
(N'Bài tập Hình tròn - Hình tam giác - Hình vuông - Hình chữ nhật', 3),
(N'Bài tập Các số 1, 2, 3', 6),
(N'Bài tập Các số 4, 5', 7),
(N'Bài tập Tách - Gộp số', 8),
(N'Bài tập Bằng nhau, nhiều hơn, ít hơn', 9),
(N'Bài tập So sánh các số: bằng, lớn hơn, bé hơn', 10),
(N'Bài tập Phép cộng', 15),
(N'Bài tập Phép trừ', 18),
(N'Bài tập Các số đến 20', 25),
(N'Bài tập Các số đến 100', 31),

-- Lớp 2
(N'Bài tập Ôn tập các số đến 100', 52),
(N'Bài tập Số hạng - Tổng', 54),
(N'Bài tập Số bị trừ - Số trừ - Hiệu', 55),
(N'Bài tập Nhiều hơn hay ít hơn bao nhiêu', 56),
(N'Bài tập Phép cộng có tổng bằng 10', 63),
(N'Bài tập 9 cộng với một số', 64),
(N'Bài tập 8 cộng với một số', 65),
(N'Bài tập Phép trừ có hiệu bằng 10', 72),
(N'Bài tập Phép nhân', 93),
(N'Bài tập Thừa số - Tích', 94),
(N'Bài tập Phép chia', 96),

-- Lớp 3
(N'Bài tập Ôn tập các số đến 1000', 111),
(N'Bài tập Cộng nhẩm, trừ nhẩm', 113),
(N'Bài tập Tìm số hạng', 114),
(N'Bài tập Tìm số bị trừ, tìm số trừ', 115),
(N'Bài tập Ôn tập phép nhân', 116),
(N'Bài tập Ôn tập phép chia', 117),
(N'Bài tập Phép nhân với số có một chữ số trong phạm vi 1000', 138),
(N'Bài tập Phép chia hết và phép chia có dư', 141),
(N'Bài tập Chia số có ba chữ số cho số có một chữ số', 143),

-- Lớp 4
(N'Bài tập Ôn tập các số đến 100000', 177),
(N'Bài tập Ôn tập phép cộng, phép trừ', 178),
(N'Bài tập Ôn tập phép nhân, phép chia', 179),
(N'Bài tập Số chẵn, số lẻ', 180),
(N'Bài tập Biểu thức có chứa chữ', 187),
(N'Bài tập Biểu thức có chứa chữ (tiếp theo)', 188),
(N'Bài tập Các số có sáu chữ số - Hàng và lớp', 201),
(N'Bài tập So sánh và xếp thứ tự các số tự nhiên', 203),
(N'Bài tập Dãy số tự nhiên', 204),
(N'Bài tập Phép nhân với số có hai chữ số', 217),
(N'Bài tập Phép chia hai số có tận cùng là chữ số 0', 219),
(N'Bài tập Phân số', 232),
(N'Bài tập Rút gọn phân số', 235),
(N'Bài tập Cộng hai phân số cùng mẫu số', 239),
(N'Bài tập Trừ hai phân số cùng mẫu số', 242),

-- Lớp 5
(N'Bài tập Ôn tập số tự nhiên và các phép tính', 265),
(N'Bài tập Ôn tập phân số', 266),
(N'Bài tập Ôn tập và bổ sung các phép tính với phân số', 267),
(N'Bài tập Tỉ số', 269),
(N'Bài tập Phép cộng hai số thập phân', 279),
(N'Bài tập Phép trừ hai số thập phân', 280),
(N'Bài tập Nhân một số thập phân với một số tự nhiên', 283),
(N'Bài tập Nhân hai số thập phân', 284),
(N'Bài tập Chia một số tự nhiên cho một số tự nhiên mà thương là số thập phân', 288),
(N'Bài tập Chia một số thập phân cho một số thập phân', 290),
(N'Bài tập Hình tam giác', 299),
(N'Bài tập Diện tích hình tam giác', 300),
(N'Bài tập Hình thang', 301),
(N'Bài tập Chu vi hình tròn', 304),
(N'Bài tập Diện tích hình tròn', 305),
(N'Bài tập Tỉ số phần trăm', 314),
(N'Bài tập Hình hộp chữ nhật, hình lập phương', 320),
(N'Bài tập Diện tích xung quanh và diện tích toàn phần của hình hộp chữ nhật', 321),
(N'Bài tập Diện tích xung quanh và diện tích toàn phần của hình lập phương', 322),
(N'Bài tập Vận tốc', 332),
(N'Bài tập Quãng đường', 333),
(N'Bài tập Thời gian', 334);

INSERT INTO Test (chapter_id, test_name) 
VALUES
-- Lớp 1
(1, N'Kiểm tra Làm quen với một số hình'),
(2, N'Kiểm tra Các số đến 10'),
(3, N'Kiểm tra Phép cộng, phép trừ trong phạm vi 10'),
(4, N'Kiểm tra Các số đến 20'),
(5, N'Kiểm tra Các số đến 100'),

-- Lớp 2
(6, N'Kiểm tra Ôn Tập và Bổ Sung'),
(7, N'Kiểm tra Phép cộng, phép trừ qua 10 trong phạm vi 20'),
(8, N'Kiểm tra Phép cộng, phép trừ có nhớ trong phạm vi 100'),
(9, N'Kiểm tra Phép Nhân, Phép Chia'),
(10, N'Kiểm tra Các Số Đến 1000'),
(11, N'Kiểm tra Phép Cộng, Phép Trừ Trong Phạm Vi 1000'),

-- Lớp 3
(12, N'Kiểm tra Ôn tập và bổ sung'),
(13, N'Kiểm tra Phép Nhân, Phép Chia trong Phạm Vi 1000'),
(14, N'Kiểm tra Các Số Đến 10,000'),
(15, N'Kiểm tra Các Số Đến 100,000'),

-- Lớp 4
(16, N'Kiểm tra Ôn Tập và Bổ Sung'),
(17, N'Kiểm tra Số tự nhiên'),
(18, N'Kiểm tra Các Phép Tính với Số Tự Nhiên'),
(19, N'Kiểm tra Phân Số'),

-- Lớp 5
(20, N'Kiểm tra Ôn Tập và Bổ Sung'),
(21, N'Kiểm tra Số Thập Phân'),
(22, N'Kiểm tra Hình Tam Giác, Hình Thang, Hình Tròn'),
(23, N'Kiểm tra Ôn Tập Học Kì 1'),
(24, N'Kiểm tra Tỉ Số Phần Trăm'),
(25, N'Kiểm tra Hình Hộp Chữ Nhật, Hình Lập Phương, Hình Trụ'),
(26, N'Kiểm tra Số Đo Thời Gian, Vận Tốc, Quãng Đường, Thời Gian'),
(27, N'Kiểm tra Ôn Tập Cuối Năm');

INSERT INTO Question (question_type, difficulty, lesson_id, img_url, question_content, pdf_solution)
VALUES
-- Multiple Choice
('multiple_choice', 1, 2, NULL, N'12 + 10 bằng bao nhiêu?', NULL),
('multiple_choice', 2, 10, NULL, N'Điền số thích hợp vào chỗ trống: 5 × __ = 25', NULL),
('multiple_choice', 2, 15, NULL, N'Hình nào có 4 cạnh bằng nhau?', NULL),
('multiple_choice', 3, 23, NULL, N'Tính diện tích hình chữ nhật có chiều dài 8 cm và chiều rộng 5 cm', NULL),

-- Fill in the Blank
('fill_in_blank', 1, 7, NULL, N'Gộp 2 con cá và 3 con cá, ta được bao nhiêu con?', NULL),
('fill_in_blank', 2, 14, NULL, N'Điền số thích hợp vào chỗ trống: 100 - __ = 85', NULL),
('fill_in_blank', 3, 22, NULL, N'Điền số vào chỗ trống: 7 + 3 = __', NULL),
('fill_in_blank', 3, 25, NULL, N'Điền số vào dãy số còn thiếu: 1, __, 3, __, 5, __', NULL),

-- Matching
('matching', 2, 5, NULL, N'Nối hình với tên gọi tương ứng', NULL),
('matching', 3, 17, NULL, N'Nối các số theo thứ tự đúng', NULL);

INSERT INTO Choice_Answer (question_id, content, is_correct, img_url)
VALUES
-- Câu hỏi: 12 + 10 = ?
(1, N'20', 1, NULL),
(1, N'21', 0, NULL),
(1, N'22', 0, NULL),
(1, N'23', 0, NULL),

-- Câu hỏi: 5 × __ = 25
(2, N'3', 0, NULL),
(2, N'4', 0, NULL),
(2, N'5', 1, NULL),
(2, N'6', 0, NULL),

-- Câu hỏi: Hình nào có 4 cạnh bằng nhau?
(3, N'Hình tròn', 0, NULL),
(3, N'Hình vuông', 1, NULL),
(3, N'Hình tam giác', 0, NULL),
(3, N'Hình chữ nhật', 0, NULL),

-- Câu hỏi: Tính diện tích hình chữ nhật (8cm x 5cm)
(4, N'35 cm²', 0, NULL),
(4, N'40 cm²', 0, NULL),
(4, N'45 cm²', 0, NULL),
(4, N'40 cm²', 1, NULL);

INSERT INTO Fill_Answer (question_id, correct_answer, position)
VALUES
(5, '5', 1), -- Gộp 2 con cá + 3 con cá
(6, '15', 1), -- 100 - 15 = 85
(7, '10', 1), -- 7 + 3 = 10
(8, '2', 1), -- Dãy số 1, 2, 3, 4, 5, 6
(8, '4', 2),
(8, '6', 3);

INSERT INTO Matching_Answer (question_id, correct_answer, img_url)
VALUES
-- Nối hình với tên gọi
(9, N'Hình hộp chữ nhật', 'https://example.com/hinh_hop.jpg'),
(9, N'Hình lập phương', 'https://example.com/hinh_lap_phuong.jpg'),

-- Nối số theo thứ tự đúng
(10, N'1', 'https://example.com/so_1.jpg'),
(10, N'2', 'https://example.com/so_2.jpg'),
(10, N'3', 'https://example.com/so_3.jpg'),
(10, N'4', 'https://example.com/so_4.jpg'),
(10, N'5', 'https://example.com/so_5.jpg');

INSERT INTO Exercise_Detail (exercise_id, question_id)
VALUES
-- Chọn đúng bài tập có lesson_id khớp với question_id
(1, 1),  -- Bài tập Vị trí (lesson_id = 2) -> Câu hỏi 1 (lesson_id = 2)
(2, 3),  -- Bài tập Khối hộp chữ nhật - Hình lập phương (lesson_id = 3) -> Câu hỏi 3 (lesson_id = 3)
(6, 5),  -- Bài tập Tách - Gộp số (lesson_id = 7) -> Câu hỏi 5 (lesson_id = 7)
(7, 6),  -- Bài tập Bằng nhau, nhiều hơn, ít hơn (lesson_id = 10) -> Câu hỏi 6 (lesson_id = 10)
(8, 10), -- Bài tập So sánh các số (lesson_id = 10) -> Câu hỏi 10 (lesson_id = 10)
(13, 2), -- Bài tập Ôn tập các số đến 100 (lesson_id = 15) -> Câu hỏi 2 (lesson_id = 15)
(15, 4), -- Bài tập Số bị trừ, số trừ (lesson_id = 22) -> Câu hỏi 4 (lesson_id = 22)
(20, 7), -- Bài tập Phép trừ có hiệu bằng 10 (lesson_id = 22) -> Câu hỏi 7 (lesson_id = 22)
(23, 8), -- Bài tập Ôn tập các số đến 1000 (lesson_id = 25) -> Câu hỏi 8 (lesson_id = 25)
(26, 9); -- Bài tập Ôn tập phép chia (lesson_id = 27) -> Câu hỏi 9 (lesson_id = 27)

INSERT INTO Test_Detail (test_id, question_id)
VALUES
(1, 1),  -- Kiểm tra Làm quen với một số hình (chapter_id = 1) → Câu hỏi 1 (lesson thuộc chapter_id = 1)
(2, 3),  -- Kiểm tra Các số đến 10 (chapter_id = 2) → Câu hỏi 3 (lesson thuộc chapter_id = 2)
(3, 5),  -- Kiểm tra Phép cộng, phép trừ trong phạm vi 10 (chapter_id = 3) → Câu hỏi 5 (lesson thuộc chapter_id = 3)
(3, 6),  -- Kiểm tra Phép cộng, phép trừ trong phạm vi 10 (chapter_id = 3) → Câu hỏi 6 (lesson thuộc chapter_id = 3)
(4, 2),  -- Kiểm tra Các số đến 20 (chapter_id = 4) → Câu hỏi 2 (lesson thuộc chapter_id = 4)
(4, 10), -- Kiểm tra Các số đến 20 (chapter_id = 4) → Câu hỏi 10 (lesson thuộc chapter_id = 4)
(7, 8),  -- Kiểm tra Phép cộng, phép trừ qua 10 trong phạm vi 20 (chapter_id = 7) → Câu hỏi 8 (lesson thuộc chapter_id = 7)
(7, 4),  -- Kiểm tra Phép cộng, phép trừ qua 10 trong phạm vi 20 (chapter_id = 7) → Câu hỏi 4 (lesson thuộc chapter_id = 7)
(9, 9),  -- Kiểm tra Phép Nhân, Phép Chia (chapter_id = 9) → Câu hỏi 9 (lesson thuộc chapter_id = 9)
(9, 7);  -- Kiểm tra Phép Nhân, Phép Chia (chapter_id = 9) → Câu hỏi 7 (lesson thuộc chapter_id = 9)

INSERT INTO Test_Result (test_id, enrollment_id, score, completion_time, done_at)
VALUES
-- Học sinh ID 6, lớp 3 (học kỳ 2 năm 2023-2024)
(12, 2, 8.0, 25, '2024-02-10'), -- Kiểm tra Ôn tập lớp 3
(13, 2, 9.0, 30, '2024-03-15'), -- Kiểm tra Phép nhân, phép chia lớp 3

-- Học sinh ID 6, lớp 4 (học kỳ 1 năm 2024-2025)
(16, 3, 7.5, 28, '2024-09-20'), -- Kiểm tra Ôn tập lớp 4
(18, 3, 8.2, 32, '2024-10-10'), -- Kiểm tra Các phép tính với số tự nhiên lớp 4

-- Học sinh ID 2, lớp 4 (học kỳ 1 năm 2024-2025)
(16, 5, 9.0, 27, '2024-09-25'), -- Kiểm tra Ôn tập lớp 4
(18, 5, 7.8, 33, '2024-10-12'), -- Kiểm tra Các phép tính với số tự nhiên lớp 4

-- Học sinh ID 3, lớp 2 (học kỳ 1 năm 2024-2025)
(7, 6, 6.5, 35, '2024-09-15'), -- Kiểm tra Phép cộng, phép trừ lớp 2
(9, 6, 7.2, 30, '2024-10-05'), -- Kiểm tra Phép nhân, phép chia lớp 2

-- Học sinh ID 5, lớp 1 (học kỳ 1 năm 2024-2025)
(2, 8, 9.5, 20, '2024-09-10'), -- Kiểm tra Các số đến 10 lớp 1
(4, 8, 8.0, 25, '2024-10-02'), -- Kiểm tra Các số đến 20 lớp 1

-- Học sinh ID 9, lớp 1 (học kỳ 1 năm 2024-2025)
(2, 11, 7.8, 30, '2024-09-18'), -- Kiểm tra Các số đến 10 lớp 1
(3, 11, 8.5, 28, '2024-10-08'); -- Kiểm tra Phép cộng, trừ trong phạm vi 10 lớp 1

INSERT INTO Exercise_Result (exercise_id, enrollment_id, score, done_at)
VALUES
-- Học sinh ID 6, lớp 3 (học kỳ 2 năm 2023-2024)
(23, 2, 8.3, '2024-02-05'), -- Bài tập Ôn tập các số đến 1000 lớp 3
(26, 2, 7.5, '2024-03-12'), -- Bài tập Ôn tập phép chia lớp 3

-- Học sinh ID 6, lớp 4 (học kỳ 1 năm 2024-2025)
(30, 3, 8.0, '2024-09-18'), -- Bài tập Ôn tập số chẵn, số lẻ lớp 4
(35, 3, 9.2, '2024-10-05'), -- Bài tập Các số có sáu chữ số lớp 4

-- Học sinh ID 2, lớp 4 (học kỳ 1 năm 2024-2025)
(30, 5, 8.8, '2024-09-22'), -- Bài tập Ôn tập số chẵn, số lẻ lớp 4
(35, 5, 7.9, '2024-10-08'), -- Bài tập Các số có sáu chữ số lớp 4

-- Học sinh ID 3, lớp 2 (học kỳ 1 năm 2024-2025)
(13, 6, 7.0, '2024-09-10'), -- Bài tập Ôn tập các số đến 100 lớp 2
(15, 6, 6.5, '2024-10-02'), -- Bài tập Số bị trừ, số trừ lớp 2

-- Học sinh ID 5, lớp 1 (học kỳ 1 năm 2024-2025)
(1, 8, 9.0, '2024-09-08'), -- Bài tập Vị trí lớp 1
(3, 8, 8.2, '2024-09-30'), -- Bài tập Hình tròn, hình vuông lớp 1

-- Học sinh ID 9, lớp 1 (học kỳ 1 năm 2024-2025)
(1, 11, 7.5, '2024-09-15'), -- Bài tập Vị trí lớp 1
(6, 11, 8.0, '2024-10-05'); -- Bài tập Tách - Gộp số lớp 1

INSERT INTO Lesson_Progress (lesson_id, enrollment_id, learning_progress, is_completed)
VALUES
-- Học sinh ID 6, lớp 3 (học kỳ 2 năm 2023-2024)
(23, 2, 100, 1), -- Ôn tập các số đến 1000
(25, 2, 85, 0),  -- Ôn tập phép nhân, phép chia

-- Học sinh ID 6, lớp 4 (học kỳ 1 năm 2024-2025)
(30, 3, 100, 1), -- Số chẵn, số lẻ
(35, 3, 70, 0),  -- Các số có sáu chữ số

-- Học sinh ID 2, lớp 4 (học kỳ 1 năm 2024-2025)
(30, 5, 90, 1), -- Số chẵn, số lẻ
(35, 5, 60, 0), -- Các số có sáu chữ số

-- Học sinh ID 3, lớp 2 (học kỳ 1 năm 2024-2025)
(13, 6, 75, 0), -- Ôn tập các số đến 100
(15, 6, 100, 1), -- Số bị trừ, số trừ

-- Học sinh ID 5, lớp 1 (học kỳ 1 năm 2024-2025)
(1, 8, 100, 1), -- Vị trí
(3, 8, 80, 0),  -- Hình tròn, hình vuông

-- Học sinh ID 9, lớp 1 (học kỳ 1 năm 2024-2025)
(1, 11, 95, 1), -- Vị trí
(6, 11, 50, 0); -- Tách - Gộp số

INSERT INTO Comment (parent_comment_id, user_id, lesson_id, comment_content, status, created_at)
VALUES
(NULL, 1, 3, N'Bài học này rất hay, giúp em phân biệt được hình tam giác với hình vuông.', N'visible', '2025-02-21 09:15:32'),
(NULL, 2, 6, N'Thầy ơi, em chưa hiểu chỗ so sánh số lớn hơn bé hơn. Giải thích thêm giúp em với!', N'visible', '2025-02-21 10:05:45'),
(NULL, 3, 13, N'Phép cộng dễ quá! Có bài tập nào khó hơn không ạ?', N'visible', '2025-02-21 11:20:10'),
(NULL, 4, 15, N'Làm sao để nhớ bảng trừ nhanh hơn ạ?', N'visible', '2025-02-21 14:30:22'),
(NULL, 5, 30, N'Em thấy bài số chẵn số lẻ khá dễ nhưng em hay nhầm. Có mẹo nào không thầy?', N'visible', '2025-02-21 16:00:00'),
-- Phản hồi
(2, 6, 6, N'Bạn có thể dùng tay để đếm thử hoặc dùng que tính để thực hành nhiều hơn nhé!', N'visible', '2025-02-21 10:30:00'),
(3, 7, 13, N'Bạn thử vào phần bài tập nâng cao trong ứng dụng, có nhiều bài khó hơn đấy!', N'visible', '2025-02-21 11:45:00'),
(4, 8, 15, N'Mình hay tập trung vào số cuối của số bị trừ, dễ nhớ hơn đó bạn!', N'visible', '2025-02-21 14:45:30'),
(5, 9, 30, N'Có mẹo nè: Số chẵn luôn có tận cùng là 0,2,4,6,8 còn số lẻ là 1,3,5,7,9.', N'visible', '2025-02-21 16:15:10');

INSERT INTO Chat (user_id, support_agent_id, created_at) 
VALUES
(1, 101, '2025-02-21 09:00:00'),
(2, 102, '2025-02-21 10:15:00'),
(3, 103, '2025-02-21 11:30:00'),
(4, 101, '2025-02-21 14:45:00');

INSERT INTO Chat_Message (chat_id, message_direction, message_content, sent_at) 
VALUES
(1, 'user', N'Chào anh/chị, em bị lỗi không vào được bài học.', '2025-02-21 09:05:00'),
(1, 'support', N'Chào em, em có thể mô tả lỗi chi tiết hơn không?', '2025-02-21 09:06:00'),
(1, 'user', N'Em nhấn vào bài mà màn hình cứ tải mãi không vào được.', '2025-02-21 09:07:30'),
(1, 'support', N'Em thử thoát ra và đăng nhập lại xem có được không?', '2025-02-21 09:09:00'),

(2, 'user', N'Em quên mật khẩu thì làm sao để lấy lại ạ?', '2025-02-21 10:16:00'),
(2, 'support', N'Em vào trang đăng nhập, chọn "Quên mật khẩu" và nhập email để nhận mã đặt lại nhé!', '2025-02-21 10:17:00'),
(2, 'user', N'Em đã làm nhưng chưa nhận được email.', '2025-02-21 10:20:00'),
(2, 'support', N'Em kiểm tra hộp thư spam hoặc đợi 5-10 phút xem nhé.', '2025-02-21 10:22:00'),

(3, 'user', N'Có cách nào học thử một bài không ạ?', '2025-02-21 11:31:00'),
(3, 'support', N'Hiện tại em có thể vào mục "Bài học miễn phí" để xem các bài học thử.', '2025-02-21 11:32:30'),
(3, 'user', N'Vậy em có thể xem những bài nào?', '2025-02-21 11:34:00'),
(3, 'support', N'Có các bài về phép cộng, phép trừ và một số bài hình học cơ bản.', '2025-02-21 11:36:00'),

(4, 'user', N'Em muốn đổi tên tài khoản thì làm thế nào?', '2025-02-21 14:46:00'),
(4, 'support', N'Em vào phần "Cài đặt tài khoản" rồi chọn chỉnh sửa tên nhé!', '2025-02-21 14:48:00');

INSERT INTO Notifications (user_id, notification_type, notification_title, notification_message, sent_at, status)
VALUES
(1, 'info', N'Cập nhật phiên bản mới', N'Phiên bản 2.1 đã ra mắt với nhiều cải tiến. Hãy cập nhật ngay!', '2025-02-22 08:00:00', 'unread'),
(2, 'info', N'Tính năng mới: Trò chơi toán học', N'Thử ngay trò chơi "Đố vui Toán học" giúp ôn luyện kiến thức một cách thú vị!', '2025-02-22 09:15:00', 'read'),
(3, 'warning', N'Bảo trì hệ thống', N'Hệ thống sẽ bảo trì từ 00:00 - 02:00 ngày 25/02. Xin lỗi vì sự bất tiện!', '2025-02-23 10:00:00', 'unread'),
(4, 'success', N'Hoàn thành bài kiểm tra', N'Chúc mừng! Bạn đã hoàn thành bài kiểm tra "Phép cộng và phép trừ".', '2025-02-23 11:45:00', 'read'),
(5, 'error', N'Lỗi kết nối', N'Hệ thống gặp sự cố, vui lòng thử lại sau.', '2025-02-23 12:30:00', 'unread'),
(7, 'success', N'Bạn đã nhận thưởng', N'Bạn vừa nhận được 10 điểm thưởng từ hệ thống. Tiếp tục cố gắng nhé!', '2025-02-23 14:10:00', 'read');

INSERT INTO Error_Report (user_id, error_message, created_at, resolved)
VALUES
-- Lỗi từ phía người dùng
(N'1', N'Không thể đăng nhập dù nhập đúng mật khẩu.', '2025-02-21 08:30:00', N'0'),
(N'2', N'Không xem được bài giảng video bài học Phép nhân hai số.', '2025-02-21 09:45:00', N'1'),
(N'3', N'Bài kiểm tra bị dừng giữa chừng của chương 2 bài 19.', '2025-02-22 10:20:00', N'0'),
(N'4', N'Ứng dụng bị treo khi chuyển bài học.', '2025-02-22 11:00:00', N'1'),
(N'5', N'Không thể tải xuống file bài tập.', '2025-02-22 15:30:00', N'0'),
(N'6', N'Tính điểm bài tập sai câu hỏi nối hai hình của bài 1.', '2025-02-23 08:15:00', N'0'),
(N'7', N'Không nhận được mã xác nhận khi đặt lại mật khẩu.', '2025-02-23 09:40:00', N'1');










