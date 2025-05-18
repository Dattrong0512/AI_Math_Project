using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseDetailResultDto
{
    public class ExerciseDetailResultForGetDto
    {
        public bool? IsCorrect { get; set; }
        public ExerciseDetailDto.ExerciseDetailDto? ExerciseDetail
        {
            get; set;
        }
    }
}
