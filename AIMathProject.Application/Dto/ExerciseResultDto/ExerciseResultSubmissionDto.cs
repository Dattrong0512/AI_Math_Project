using System.Collections.Generic;

namespace AIMathProject.Application.Dto.ExerciseDetailResultDto
{
    public class ExerciseResultSubmissionDto
    {
        public List<ExerciseDetailResultDto> DetailResults { get; set; } = new List<ExerciseDetailResultDto>();

        public int CompletionTime { get; set; }
    }
}