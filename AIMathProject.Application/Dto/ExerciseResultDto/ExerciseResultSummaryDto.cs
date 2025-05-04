using AIMathProject.Domain.Entities;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseResultDto
{
    public class ExerciseResultSummaryDto
    {
        public decimal? Score { get; set; }

        public List<ExerciseDetailResultDto.ExerciseDetailResultSummaryDto> ExerciseDetailResults { get; set; } = new List<ExerciseDetailResultDto.ExerciseDetailResultSummaryDto>();
    }
}
