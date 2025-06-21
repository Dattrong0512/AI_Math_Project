using AIMathProject.Domain.Entities;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseResultDto
{
    public class ExerciseResultDto
    {
        public int? ExerciseId { get; set; }

        public int? EnrollmentId { get; set; }

        public decimal? Score { get; set; }

        public int? CompletionTime { get; set; }

        public DateTime? DoneAt { get; set; }

        public List<ExerciseDetailResultDto.ExerciseDetailResultForGetDto> ExerciseDetailResults { get; set; } = new List<ExerciseDetailResultDto.ExerciseDetailResultForGetDto>();
    }
}
