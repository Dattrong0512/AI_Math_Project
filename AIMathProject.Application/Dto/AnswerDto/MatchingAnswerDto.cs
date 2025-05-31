using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.AnswerDto
{
    public class MatchingAnswerDto
    {
        public int AnswerId { get; set; }

        public string AnswerContent1 { get; set; } = null!;

        public string AnswerContent2 { get; set; } = null!;

    }
}
