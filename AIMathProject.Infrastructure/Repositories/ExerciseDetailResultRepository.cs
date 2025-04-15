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

        public async Task<bool> UpsertExerciseDetailResult(int enrollment_id, int lesson_order, List<ExerciseDetailResultDto> edrDtoList)
        {
            var exerciseResultId = (from er in _context.ExerciseResults
                                    join e in _context.Exercises on er.ExerciseId equals e.ExerciseId
                                    join l in _context.Lessons on e.LessonId equals l.LessonId
                                    where er.EnrollmentId == enrollment_id && l.LessonOrder == lesson_order
                                    select er.ExerciseResultId).FirstOrDefault();

            if (exerciseResultId == 0)
            {
                ExerciseResult exerciseResult;
                exerciseResult = new ExerciseResult
                {
                    ExerciseId = (from e in _context.Exercises
                                  join l in _context.Lessons on e.LessonId equals l.LessonId
                                  where l.LessonOrder == lesson_order
                                  select e.ExerciseId).FirstOrDefault(),
                    EnrollmentId = enrollment_id,
                };

                await _context.ExerciseResults.AddAsync(exerciseResult);
                await _context.SaveChangesAsync();

                exerciseResultId = (from er in _context.ExerciseResults
                                    join e in _context.Exercises on er.ExerciseId equals e.ExerciseId
                                    join l in _context.Lessons on e.LessonId equals l.LessonId
                                    where er.EnrollmentId == enrollment_id && l.LessonOrder == lesson_order
                                    select er.ExerciseResultId).FirstOrDefault();
            }

            foreach (var edrItem in edrDtoList)
            {
                var exerciseDetailId = (from ed in _context.ExerciseDetails
                                        join e in _context.Exercises on ed.ExerciseId equals e.ExerciseId
                                        join l in _context.Lessons on e.LessonId equals l.LessonId
                                        where ed.QuestionId == edrItem.QuestionId && l.LessonOrder == lesson_order
                                        select ed.ExerciseDetailId).FirstOrDefault();

                if (exerciseDetailId == 0)
                {
                    throw new Exception($"ExerciseDetail not found for QuestionId: {edrItem.QuestionId}");
                }

                var existingResult = _context.ExerciseDetailResults
                .FirstOrDefault(edr => edr.ExerciseDetailId == exerciseDetailId && edr.ExerciseResultId == exerciseResultId);

                if (existingResult != null)
                {
                    existingResult.IsCorrect = edrItem.IsCorrect;
                    _context.ExerciseDetailResults.Update(existingResult);
                }
                else
                {
                    ExerciseDetailResult edr = edrItem.ToExerciseDetailResultFromExerciseDetailResultDto(exerciseDetailId, exerciseResultId);
                    await _context.ExerciseDetailResults.AddAsync(edr);
                }
            };
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
