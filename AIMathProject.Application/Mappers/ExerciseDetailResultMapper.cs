using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Application.Dto.ExerciseDetailDto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Domain.Entities;

namespace AIMathProject.Application.Mappers
{
    public static class ExerciseDetailResultMappers
    {
        public static ExerciseDetailResultDto ToExerciseDetailResultDto(this ExerciseDetailResult entity, int question_id)
        {
            return new ExerciseDetailResultDto
            {
                QuestionId = question_id,
                IsCorrect = entity.IsCorrect,
            };
        }

        public static ExerciseDetailResultDto ToExerciseDetailResultDtoForGet(this ExerciseDetailResult entity)
        {
            return new ExerciseDetailResultDto
            {
                ExerciseDetailId = entity.ExerciseDetailId,
                ExerciseResultId = entity.ExerciseResultId,
                IsCorrect = entity.IsCorrect,
                ExerciseDetail = entity.ExerciseDetail?.ToExerciseDetailDto()
            };
        }

        public static ExerciseDetailResult ToExerciseDetailResultFromExerciseDetailResultDto(this ExerciseDetailResultDto dto, int exercise_detail_id, int exercise_result_id)
        {
            return new ExerciseDetailResult
            {
                ExerciseDetailId = exercise_detail_id,
                ExerciseResultId = exercise_result_id,
                IsCorrect = dto.IsCorrect,
            };
        }

        public static List<ExerciseDetailResultDto> ToExerciseDetailResultDtoList(this List<ExerciseDetailResult> Listedr)
        {
            List<ExerciseDetailResultDto> listLPDto = new List<ExerciseDetailResultDto> { };
            foreach (var qs in Listedr)
            {
                listLPDto.Add(qs.ToExerciseDetailResultDtoForGet());
            }
            return listLPDto;
        }
    }
}
