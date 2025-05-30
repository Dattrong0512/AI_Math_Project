using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.EnrollmentUnlockExerciseDto
{
    public class EnrollmentUnlockExerciseDto
    {
        public DateTime? UnlockDate { get; set; }

        public virtual Exercise? Exercise { get; set; }
    }
}
