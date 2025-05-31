using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Entities;
using AIMathProject.Application.Dto;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ExerciseDetailResultRepository : IExerciseDetailResultRepository<ExerciseDetailResultDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseDetailResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpsertExerciseDetailResult(int enrollment_id, int exerciseId, List<ExerciseDetailResultDto> edrDtoList)
        {
            // Tìm ExerciseResult dựa trên enrollmentId và exerciseId
            var exerciseResultId = (from er in _context.ExerciseResults
                                    where er.EnrollmentId == enrollment_id && er.ExerciseId == exerciseId
                                    select er.ExerciseResultId).FirstOrDefault();

            // Nếu chưa có ExerciseResult, tạo mới
            if (exerciseResultId == 0)
            {
                ExerciseResult exerciseResult = new ExerciseResult
                {
                    ExerciseId = exerciseId,
                    EnrollmentId = enrollment_id,
                };

                await _context.ExerciseResults.AddAsync(exerciseResult);
                await _context.SaveChangesAsync();

                // Lấy ID của ExerciseResult vừa tạo
                exerciseResultId = (from er in _context.ExerciseResults
                                    where er.EnrollmentId == enrollment_id && er.ExerciseId == exerciseId
                                    select er.ExerciseResultId).FirstOrDefault();
            }

            // Cập nhật hoặc thêm mới từng ExerciseDetailResult
            foreach (var edrItem in edrDtoList)
            {
                // Tìm ExerciseDetail dựa trên questionId và exerciseId
                var exerciseDetailId = (from ed in _context.ExerciseDetails
                                        where ed.QuestionId == edrItem.QuestionId && ed.ExerciseId == exerciseId
                                        select ed.ExerciseDetailId).FirstOrDefault();

                if (exerciseDetailId == 0)
                {
                    throw new Exception($"ExerciseDetail not found for QuestionId: {edrItem.QuestionId} and ExerciseId: {exerciseId}");
                }

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

                    // Xóa tất cả UserFillAnswers cũ nếu có
                    var existingUserFillAnswers = _context.UserFillAnswers
                        .Where(ufa => ufa.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserFillAnswers.Any())
                    {
                        _context.UserFillAnswers.RemoveRange(existingUserFillAnswers);
                    }

                    //Xóa tất cả UserChoiceAnswers cũ nêu sco
                    var existingUserChoiceAnswers = _context.UserChoiceAnswers
                        .Where(uca => uca.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserChoiceAnswers.Any())
                    {
                        _context.UserChoiceAnswers.RemoveRange(existingUserChoiceAnswers);
                    }

                    //Tương tự với UserMatchingAnswers
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

                // Xử lý dựa vào QuestionType
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
                else if ((edrItem.QuestionType == "fill_in_blank" || edrItem.QuestionType == "vertical_calculation") && edrItem.UserFillAnswers != null && edrItem.UserFillAnswers.Any())
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
                else if ((edrItem.QuestionType == "text_image_matching" || edrItem.QuestionType == "image_matching") && edrItem.UserMatchingAnswers != null && edrItem.UserMatchingAnswers.Any()){
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

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

