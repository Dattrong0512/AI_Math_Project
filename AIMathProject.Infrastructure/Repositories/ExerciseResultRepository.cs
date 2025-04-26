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

namespace AIMathProject.Infrastructure.Repositories
{
    public class ExerciseResultRepository : IExerciseResultRepository<ExerciseResultDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExerciseResultDto> GetDetailExerciseResultById(int enrollment_id, int lesson_order)
        {
            var exerciseResultId = await (from er1 in _context.ExerciseResults
                                          join e in _context.Exercises on er1.ExerciseId equals e.ExerciseId
                                          join l in _context.Lessons on e.LessonId equals l.LessonId
                                          where er1.EnrollmentId == enrollment_id && l.LessonOrder == lesson_order
                                          select er1.ExerciseResultId).FirstOrDefaultAsync();
            if (exerciseResultId == 0)
            {
                throw new Exception("ExerciseResult not found.");
            }

            var questionId = (from er2 in _context.ExerciseResults
                              join edr in _context.ExerciseDetailResults 
                              on er2.ExerciseResultId equals edr.ExerciseResultId
                              join ed in _context.ExerciseDetails 
                              on edr.ExerciseDetailId equals ed.ExerciseDetailId
                              where er2.ExerciseResultId == exerciseResultId
                              select ed.QuestionId)
                          .ToList();

            var questionList = await _context.Questions
                .Where(qs => questionId.Contains(qs.QuestionId))
                .ToListAsync();

            var edrId = (from er3 in _context.ExerciseResults
                              join edr in _context.ExerciseDetailResults
                              on er3.ExerciseResultId equals edr.ExerciseResultId
                            where er3.ExerciseResultId == exerciseResultId
                         select edr.ExerciseDetailResultId)
                          .ToList();
            var edrList = await _context.ExerciseDetailResults
                .Where(qs => edrId.Contains(qs.ExerciseDetailResultId))
                .ToListAsync();

            var exerciseDetailId = (from er4 in _context.ExerciseResults
                                  join edr in _context.ExerciseDetailResults
                                  on er4.ExerciseResultId equals edr.ExerciseResultId
                                  join ed in _context.ExerciseDetails
                                  on edr.ExerciseDetailId equals ed.ExerciseDetailId
                                  where er4.ExerciseResultId == exerciseResultId
                                    select ed.ExerciseDetailId).ToList();

            var exerciseDetailList = await _context.ExerciseDetails
                .Where(qs => exerciseDetailId.Contains(qs.ExerciseDetailId))
                .ToListAsync();

            List<ExerciseDetailResult> edrListReturn = new List<ExerciseDetailResult>() { };

            var exerciseDetailDictionary = exerciseDetailList.ToDictionary(q => q.ExerciseDetailId);

            var questionDictionary = questionList.ToDictionary(q => q.QuestionId);

            foreach (var edr in edrList)
            {
                if (edr.ExerciseDetailId.HasValue && exerciseDetailDictionary.TryGetValue((int)edr.ExerciseDetailId, out var exerciseDetail))
                {
                    edr.ExerciseDetail = exerciseDetail;
                }
                else
                {
                    throw new Exception($"ExerciseDetail not found for ExerciseDetailId: {edr.ExerciseDetailId}");
                }
                if (edr.ExerciseDetail != null && edr.ExerciseDetail.QuestionId.HasValue)
                {
                    var questionIdtemp = edr.ExerciseDetail.QuestionId.Value;
                    if (questionDictionary.TryGetValue(questionIdtemp, out var question))
                    {
                        edr.ExerciseDetail.Question = question;
                    }
                }
            }
            foreach (var edr in edrList)
            {
                if (edr.ExerciseDetail?.Question?.QuestionType == "multiple_choice")
                {
                    var Answer = _context.ChoiceAnswers
                        .Where(choice => choice.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.ChoiceAnswers = Answer;

                }
                else if (edr.ExerciseDetail?.Question?.QuestionType == "matching")
                {
                    var Answer = _context.MatchingAnswers
                        .Where(match => match.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.MatchingAnswers = Answer;

                }
                else if (edr.ExerciseDetail?.Question?.QuestionType == "fill_in_blank")
                {
                    var Answer = _context.FillAnswers
                        .Where(match => match.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.FillAnswers = Answer;

                }
                edrListReturn.Add(edr);
            }

            var er = await _context.ExerciseResults
                   .Where(er => er.ExerciseResultId == exerciseResultId)
                   .Select(er => new ExerciseResultDto
                   {
                       ExerciseId = er.ExerciseId,
                       EnrollmentId = er.EnrollmentId,
                       Score = er.Score,
                       DoneAt = er.DoneAt,
                       ExerciseDetailResults = edrListReturn.ToExerciseDetailResultDtoList(),
                   })
                   .FirstOrDefaultAsync();

            return er ?? new ExerciseResultDto();
        }

        public async Task<List<ExerciseResultDto>> GetAllExerciseResultsByEnrollmentId(int enrollmentId)
        {
            var exerciseResultIds = await _context.ExerciseResults
                .Where(er => er.EnrollmentId == enrollmentId)
                .Select(er => er.ExerciseResultId)
                .ToListAsync();

            if (!exerciseResultIds.Any())
            {
                throw new Exception("No ExerciseResults found for the given EnrollmentId.");
            }

            var questionIds = await (from er in _context.ExerciseResults
                                     join edr in _context.ExerciseDetailResults on er.ExerciseResultId equals edr.ExerciseResultId
                                     join ed in _context.ExerciseDetails on edr.ExerciseDetailId equals ed.ExerciseDetailId
                                     where exerciseResultIds.Contains(er.ExerciseResultId)
                                     select ed.QuestionId)
                                     .ToListAsync();

            var questionList = await _context.Questions
                .Where(q => questionIds.Contains(q.QuestionId))
                .ToListAsync();

            // Lấy danh sách ExerciseDetailResultId
            var edrIds = await (from er in _context.ExerciseResults
                                join edr in _context.ExerciseDetailResults on er.ExerciseResultId equals edr.ExerciseResultId
                                where exerciseResultIds.Contains(er.ExerciseResultId)
                                select edr.ExerciseDetailResultId)
                                .ToListAsync();

            var edrList = await _context.ExerciseDetailResults
                .Where(edr => edrIds.Contains(edr.ExerciseDetailResultId))
                .ToListAsync();

            var exerciseDetailIds = await (from er in _context.ExerciseResults
                                           join edr in _context.ExerciseDetailResults on er.ExerciseResultId equals edr.ExerciseResultId
                                           join ed in _context.ExerciseDetails on edr.ExerciseDetailId equals ed.ExerciseDetailId
                                           where exerciseResultIds.Contains(er.ExerciseResultId)
                                           select ed.ExerciseDetailId)
                                            .ToListAsync();

            var exerciseDetailList = await _context.ExerciseDetails
                .Where(ed => exerciseDetailIds.Contains(ed.ExerciseDetailId))
                .ToListAsync();

            var exerciseDetailDictionary = exerciseDetailList.ToDictionary(ed => ed.ExerciseDetailId);
            var questionDictionary = questionList.ToDictionary(q => q.QuestionId);

            foreach (var edr in edrList)
            {
                if (edr.ExerciseDetailId.HasValue && exerciseDetailDictionary.TryGetValue(edr.ExerciseDetailId.Value, out var exerciseDetail))
                {
                    edr.ExerciseDetail = exerciseDetail;
                }

                if (edr.ExerciseDetail?.QuestionId.HasValue == true && questionDictionary.TryGetValue(edr.ExerciseDetail.QuestionId.Value, out var question))
                {
                    edr.ExerciseDetail.Question = question;
                }
            }

            foreach (var edr in edrList)
            {
                if (edr.ExerciseDetail?.Question?.QuestionType == "multiple_choice")
                {
                    var answers = _context.ChoiceAnswers
                        .Where(ca => ca.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.ChoiceAnswers = answers;
                }
                else if (edr.ExerciseDetail?.Question?.QuestionType == "matching")
                {
                    var answers = _context.MatchingAnswers
                        .Where(ma => ma.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.MatchingAnswers = answers;
                }
                else if (edr.ExerciseDetail?.Question?.QuestionType == "fill_in_blank")
                {
                    var answers = _context.FillAnswers
                        .Where(fa => fa.QuestionId == edr.ExerciseDetail.Question.QuestionId)
                        .ToList();
                    edr.ExerciseDetail.Question.FillAnswers = answers;
                }
            }

            // Tạo danh sách ExerciseResultDto
            var exerciseResults = await _context.ExerciseResults
                .Where(er => exerciseResultIds.Contains(er.ExerciseResultId))
                .Select(er => new ExerciseResultDto
                {
                    ExerciseId = er.ExerciseId,
                    EnrollmentId = er.EnrollmentId,
                    Score = er.Score,
                    DoneAt = er.DoneAt,
                    ExerciseDetailResults = edrList.ToExerciseDetailResultDtoList()
                })
                .ToListAsync();

            return exerciseResults;
        }
    }
}
