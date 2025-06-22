using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ExerciseDetailResultRepository : IExerciseDetailResultRepository<ExerciseDetailResultDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseDetailResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpsertExerciseDetailResult(int enrollment_id, int exerciseId, List<ExerciseDetailResultDto> edrDtoList, int completionTime)
        {
            // Tìm ExerciseResult dựa trên enrollmentId và exerciseId
            var exerciseResult = _context.ExerciseResults
                        .FirstOrDefault(er => er.EnrollmentId == enrollment_id && er.ExerciseId == exerciseId);

            // Nếu chưa có ExerciseResult, tạo mới
            if (exerciseResult == null)
            {
                exerciseResult = new ExerciseResult
                {
                    ExerciseId = exerciseId,
                    EnrollmentId = enrollment_id,
                    CompletionTime = completionTime,
                    DoneAt = DateTime.UtcNow,
                };

                await _context.ExerciseResults.AddAsync(exerciseResult);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Cập nhật CompletionTime
                exerciseResult.CompletionTime = completionTime;
                exerciseResult.DoneAt = DateTime.UtcNow;
                _context.ExerciseResults.Update(exerciseResult);
                await _context.SaveChangesAsync();
            }

            int exerciseResultId = exerciseResult.ExerciseResultId;

            // LẤY TẤT CẢ CÁC EXERCISE DETAIL CỦA BÀI TẬP
            var allExerciseDetails = _context.ExerciseDetails
                .Where(ed => ed.ExerciseId == exerciseId)
                .ToList();

            // Câu hỏi đã có kq
            var submittedQuestionIds = edrDtoList.Select(edr => edr.QuestionId).ToHashSet();

            // XỬ LÝ CÁC CÂU HỎI ĐÃ CÓ KẾT QUẢ
            foreach (var edrItem in edrDtoList)
            {
                // Tìm ExerciseDetail dựa trên questionId và exerciseId
                var exerciseDetail = allExerciseDetails.FirstOrDefault(ed => ed.QuestionId == edrItem.QuestionId);

                if (exerciseDetail == null)
                {
                    throw new Exception($"ExerciseDetail not found for QuestionId: {edrItem.QuestionId} and ExerciseId: {exerciseId}");
                }

                int exerciseDetailId = exerciseDetail.ExerciseDetailId;

                // Kiểm tra đã có kết quả chưa
                var existingResult = _context.ExerciseDetailResults
                    .FirstOrDefault(edr => edr.ExerciseDetailId == exerciseDetailId && edr.ExerciseResultId == exerciseResultId);

                int exerciseDetailResultId;

                if (existingResult != null)
                {
                    // Cập nhật kết quả hiện có
                    existingResult.IsCorrect = edrItem.IsCorrect;
                    existingResult.QuestionType = edrItem.QuestionType;

                    _context.ExerciseDetailResults.Update(existingResult);
                    exerciseDetailResultId = existingResult.ExerciseDetailResultId;

                    // Xóa các câu trả lời cũ
                    var existingUserFillAnswers = _context.UserFillAnswers
                        .Where(ufa => ufa.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserFillAnswers.Any())
                    {
                        _context.UserFillAnswers.RemoveRange(existingUserFillAnswers);
                    }

                    var existingUserChoiceAnswers = _context.UserChoiceAnswers
                        .Where(uca => uca.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserChoiceAnswers.Any())
                    {
                        _context.UserChoiceAnswers.RemoveRange(existingUserChoiceAnswers);
                    }

                    var existingUserMatchingAnswers = _context.UserMatchingAnswers
                        .Where(uma => uma.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserMatchingAnswers.Any())
                    {
                        _context.UserMatchingAnswers.RemoveRange(existingUserMatchingAnswers);
                    }
                }
                else
                {
                    // Thêm mới kết quả
                    ExerciseDetailResult edr = edrItem.ToExerciseDetailResultFromExerciseDetailResultDto(exerciseDetailId, exerciseResultId);
                    await _context.ExerciseDetailResults.AddAsync(edr);
                    await _context.SaveChangesAsync();
                    exerciseDetailResultId = edr.ExerciseDetailResultId;
                }

                // Xử lý dựa vào QuestionType và lưu chi tiết câu trả lời
                if ((edrItem.QuestionType == "multiple_choice" || edrItem.QuestionType == "single_choice") && edrItem.UserChoiceAnswers != null && edrItem.UserChoiceAnswers.Any())
                {
                    // Thêm UserChoiceAnswers
                    foreach (var userChoiceAnswerDto in edrItem.UserChoiceAnswers)
                    {
                        var userChoiceAnswer = new UserChoiceAnswer
                        {
                            ExerciseDetailResultId = exerciseDetailResultId,
                            AnswerId = userChoiceAnswerDto.AnswerId,
                            IsCorrect = userChoiceAnswerDto.IsCorrect
                        };

                        await _context.UserChoiceAnswers.AddAsync(userChoiceAnswer);
                    }
                }
                else if ((edrItem.QuestionType == "fill_in_blank" || edrItem.QuestionType == "add" || edrItem.QuestionType == "sub" || edrItem.QuestionType == "multi" || edrItem.QuestionType == "div") && edrItem.UserFillAnswers != null && edrItem.UserFillAnswers.Any())
                {
                    // Thêm UserFillAnswers
                    foreach (var userFillAnswerDto in edrItem.UserFillAnswers)
                    {
                        var userFillAnswer = new UserFillAnswer
                        {
                            ExerciseDetailResultId = exerciseDetailResultId,
                            WrongAnswer = userFillAnswerDto.WrongAnswer,
                            Position = userFillAnswerDto.Position
                        };

                        await _context.UserFillAnswers.AddAsync(userFillAnswer);
                    }
                }
                else if ((edrItem.QuestionType == "text_image_matching" || edrItem.QuestionType == "image_matching") && edrItem.UserMatchingAnswers != null && edrItem.UserMatchingAnswers.Any())
                {
                    foreach (var UserMatchingAnswerDto in edrItem.UserMatchingAnswers)
                    {
                        var userMatchingAnswer = new UserMatchingAnswer
                        {
                            ExerciseDetailResultId = exerciseDetailResultId,
                            AnswerContent1 = UserMatchingAnswerDto.AnswerContent1,
                            AnswerContent2 = UserMatchingAnswerDto.AnswerContent2
                        };

                        await _context.UserMatchingAnswers.AddAsync(userMatchingAnswer);
                    }
                }
            }

            // XỬ LÝ CÁC CÂU HỎI CHƯA CÓ KẾT QUẢ TỰ CHUYỂN THÀNH FASLE
            foreach (var exerciseDetail in allExerciseDetails)
            {
                if (!submittedQuestionIds.Contains(exerciseDetail.QuestionId))
                {
                    // Tìm ExerciseDetailResult 
                    var existingResult = _context.ExerciseDetailResults
                        .FirstOrDefault(edr => edr.ExerciseDetailId == exerciseDetail.ExerciseDetailId && edr.ExerciseResultId == exerciseResultId);

                    if (existingResult != null)
                    {
                        // Cập nhật thành sai
                        existingResult.IsCorrect = false;
                        _context.ExerciseDetailResults.Update(existingResult);

                        // Xóa các chi tiết câu trả lời cũ nếu có
                        var existingUserFillAnswers = _context.UserFillAnswers
                            .Where(ufa => ufa.ExerciseDetailResultId == existingResult.ExerciseDetailResultId);
                        if (existingUserFillAnswers.Any())
                        {
                            _context.UserFillAnswers.RemoveRange(existingUserFillAnswers);
                        }

                        var existingUserChoiceAnswers = _context.UserChoiceAnswers
                            .Where(uca => uca.ExerciseDetailResultId == existingResult.ExerciseDetailResultId);
                        if (existingUserChoiceAnswers.Any())
                        {
                            _context.UserChoiceAnswers.RemoveRange(existingUserChoiceAnswers);
                        }

                        var existingUserMatchingAnswers = _context.UserMatchingAnswers
                            .Where(uma => uma.ExerciseDetailResultId == existingResult.ExerciseDetailResultId);
                        if (existingUserMatchingAnswers.Any())
                        {
                            _context.UserMatchingAnswers.RemoveRange(existingUserMatchingAnswers);
                        }
                    }
                    else
                    {
                        var question = _context.Questions.FirstOrDefault(q => q.QuestionId == exerciseDetail.QuestionId);
                        string questionType = question?.QuestionType ?? "unknown";

                        // Tạo mới kết quả với IsCorrect = false
                        var newDetailResult = new ExerciseDetailResult
                        {
                            ExerciseDetailId = exerciseDetail.ExerciseDetailId,
                            ExerciseResultId = exerciseResultId,
                            IsCorrect = false,
                            QuestionType = questionType
                        };

                        await _context.ExerciseDetailResults.AddAsync(newDetailResult);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
