using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Application.Dto.ExerciseResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Entities;
using AIMathProject.Application.Dto;
using System.Diagnostics;
using AIMathProject.Application.Dto.AnswerDto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Application.Dto.ExerciseDetailDto;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ExerciseResultRepository : IExerciseResultRepository<ExerciseResultDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExerciseResultDto> GetDetailExerciseResultById(int enrollment_id, int exerciseId)
        {
            // Lấy ExerciseResult dựa trên enrollment_id và exerciseId
            var exerciseResult = await (from er in _context.ExerciseResults
                                        where er.EnrollmentId == enrollment_id && er.ExerciseId == exerciseId
                                        select er)
                                      .Include(er => er.ExerciseDetailResults)
                                          .ThenInclude(edr => edr.ExerciseDetail)
                                              .ThenInclude(ed => ed.Question)
                                                  .ThenInclude(q => q.ChoiceAnswers)
                                      .Include(er => er.ExerciseDetailResults)
                                          .ThenInclude(edr => edr.ExerciseDetail)
                                              .ThenInclude(ed => ed.Question)
                                                  .ThenInclude(q => q.MatchingAnswers)
                                      .Include(er => er.ExerciseDetailResults)
                                          .ThenInclude(edr => edr.ExerciseDetail)
                                              .ThenInclude(ed => ed.Question)
                                                  .ThenInclude(q => q.FillAnswers)
                                      .Include(er => er.ExerciseDetailResults)
                                              .ThenInclude(edr => edr.UserFillAnswers)
                                      .Include(er => er.ExerciseDetailResults)
                                              .ThenInclude(edr => edr.UserChoiceAnswers)
                                      .Include(er => er.ExerciseDetailResults)
                                               .ThenInclude(edr => edr.UserMatchingAnswers)
                                      .FirstOrDefaultAsync();

            if (exerciseResult == null)
            {
                throw new Exception("ExerciseResult not found.");
            }

            // Ánh xạ sang DTO
            var exerciseResultDto = new ExerciseResultDto
            {
                ExerciseId = exerciseResult.ExerciseId,
                EnrollmentId = exerciseResult.EnrollmentId,
                Score = exerciseResult.Score,
                DoneAt = exerciseResult.DoneAt,
                ExerciseDetailResults = exerciseResult.ExerciseDetailResults
                    .Select(edr => new ExerciseDetailResultForGetDto
                    {
                        IsCorrect = edr.IsCorrect,
                        QuestionType = edr.QuestionType,
                        UserChoiceAnswers = edr.UserChoiceAnswers != null ?
                            edr.UserChoiceAnswers.Select(uca => new UserChoiceAnswerDto
                            {
                                AnswerId = uca.AnswerId,
                                IsCorrect = uca.IsCorrect
                            }).ToList() :
                            new List<UserChoiceAnswerDto>(),
                        UserFillAnswers = edr.UserFillAnswers != null ?
                            edr.UserFillAnswers.Select(ufa => new UserFillAnswerDto
                            {
                                WrongAnswer = ufa.WrongAnswer,
                                Position = ufa.Position
                            }).ToList() :
                            new List<UserFillAnswerDto>(),
                        UserMatchingAnswers = edr.UserMatchingAnswers != null ?
                            edr.UserMatchingAnswers.Select(uma => new UserMatchingAnswerDto
                            {
                                AnswerContent1 = uma.AnswerContent1,
                                AnswerContent2 = uma.AnswerContent2
                            }).ToList() :
                            new List<UserMatchingAnswerDto>(),
                        ExerciseDetail = edr.ExerciseDetail != null
                            ? new ExerciseDetailDto
                            {
                                Question = edr.ExerciseDetail.Question != null
                                    ? edr.ExerciseDetail.Question.ToQuestionDto()
                                    : null
                            }
                            : null
                    })
                    .ToList()
            };

            return exerciseResultDto;
        }

        public async Task<List<ExerciseResultDto>> GetAllExerciseResultsByEnrollmentId(int enrollmentId)
        {
            var exerciseResults = await _context.ExerciseResults
                .Where(er => er.EnrollmentId == enrollmentId)
                .Include(er => er.ExerciseDetailResults)
                    .ThenInclude(edr => edr.ExerciseDetail)
                        .ThenInclude(ed => ed.Question)
                            .ThenInclude(q => q.ChoiceAnswers)
                .Include(er => er.ExerciseDetailResults)
                    .ThenInclude(edr => edr.ExerciseDetail)
                        .ThenInclude(ed => ed.Question)
                            .ThenInclude(q => q.MatchingAnswers)
                .Include(er => er.ExerciseDetailResults)
                    .ThenInclude(edr => edr.ExerciseDetail)
                        .ThenInclude(ed => ed.Question)
                            .ThenInclude(q => q.FillAnswers)
                .Include(er => er.ExerciseDetailResults)
                        .ThenInclude(edr => edr.UserFillAnswers)
                .Include(er => er.ExerciseDetailResults)
                        .ThenInclude(edr => edr.UserChoiceAnswers)
                .Include(er => er.ExerciseDetailResults)
                        .ThenInclude(edr => edr.UserMatchingAnswers)
                .ToListAsync();

            if (!exerciseResults.Any())
            {
                throw new Exception("ExerciseResults not found.");
            }

            var exerciseResultDtos = exerciseResults.Select(er => new ExerciseResultDto
            {
                ExerciseId = er.ExerciseId,
                EnrollmentId = er.EnrollmentId,
                Score = er.Score,
                DoneAt = er.DoneAt,
                ExerciseDetailResults = er.ExerciseDetailResults
                    .Select(edr => new ExerciseDetailResultForGetDto
                    {
                        IsCorrect = edr.IsCorrect,
                        QuestionType = edr.QuestionType,
                        UserChoiceAnswers = edr.UserChoiceAnswers != null ?
                            edr.UserChoiceAnswers.Select(uca => new UserChoiceAnswerDto
                            {
                                AnswerId = uca.AnswerId,
                                IsCorrect = uca.IsCorrect
                            }).ToList() :
                            new List<UserChoiceAnswerDto>(),
                        UserFillAnswers = edr.UserFillAnswers != null ?
                            edr.UserFillAnswers.Select(ufa => new UserFillAnswerDto
                            {
                                WrongAnswer = ufa.WrongAnswer,
                                Position = ufa.Position
                            }).ToList() :
                            new List<UserFillAnswerDto>(),

                        UserMatchingAnswers = edr.UserMatchingAnswers != null ?
                            edr.UserMatchingAnswers.Select(uma => new UserMatchingAnswerDto
                            {
                                AnswerContent1 = uma.AnswerContent1,
                                AnswerContent2 = uma.AnswerContent2
                            }).ToList() : 
                            new List<UserMatchingAnswerDto>(),

                        ExerciseDetail = edr.ExerciseDetail != null
                            ? new ExerciseDetailDto
                            {
                                Question = edr.ExerciseDetail.Question != null
                                    ? edr.ExerciseDetail.Question.ToQuestionDto()
                                    : null
                            }
                            : null
                    })
                    .ToList()
            }).ToList();

            return exerciseResultDtos;
        }
    }
}
