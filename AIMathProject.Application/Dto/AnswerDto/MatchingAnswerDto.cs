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

        public string CorrectAnswer { get; set; } = null!;

        public string ImgUrl { get; set; } = null!;

    }
}
