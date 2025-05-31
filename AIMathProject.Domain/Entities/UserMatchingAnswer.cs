using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Entities
{
    public partial class UserMatchingAnswer
    {   
        public int UserMatchingAnswerId { get; set; }

        public int? ExerciseDetailResultId { get; set; }

        public string? AnswerContent1 { get; set; }

        public string? AnswerContent2 { get; set; }

        public virtual ExerciseDetailResult? ExerciseDetailResult { get; set; }
    }
}
