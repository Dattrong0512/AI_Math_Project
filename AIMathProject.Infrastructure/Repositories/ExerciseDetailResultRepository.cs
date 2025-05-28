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
                    // Nếu là loại câu hỏi multiple choice thì cập nhật ChoiceAnswerId
                    if (edrItem.QuestionType == "multiple_choice" && edrItem.ChoiceAnswerId.HasValue)
                    {
                        existingResult.ChoiceAnswerId = edrItem.ChoiceAnswerId;
                    }
                    _context.ExerciseDetailResults.Update(existingResult);
                    exerciseDetailResultId = existingResult.ExerciseDetailResultId;

                    // Xóa tất cả UserFillAnswers cũ nếu có
                    var existingUserFillAnswers = _context.UserFillAnswers
                        .Where(ufa => ufa.ExerciseDetailResultId == exerciseDetailResultId);
                    if (existingUserFillAnswers.Any())
                    {
                        _context.UserFillAnswers.RemoveRange(existingUserFillAnswers);
                    }
                }
                else
                {
                    // Thêm mới kết quả
                    ExerciseDetailResult edr = edrItem.ToExerciseDetailResultFromExerciseDetailResultDto(exerciseDetailId, exerciseResultId);
                    await _context.ExerciseDetailResults.AddAsync(edr);
                    await _context.SaveChangesAsync(); // Lưu để lấy ID mới tạo
                    exerciseDetailResultId = edr.ExerciseDetailResultId;
                }

                // Thêm UserFillAnswers nếu có
                if (edrItem.UserFillAnswers != null && edrItem.UserFillAnswers.Any())
                {
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
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

