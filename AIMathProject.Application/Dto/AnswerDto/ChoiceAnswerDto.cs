using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.AnswerDto
{
    public class ChoiceAnswerDto
    {
        public int AnswerId { get; set; }

        public string? Content { get; set; }

        public bool? IsCorrect { get; set; }

        public string? ImgUrl { get; set; }
    }
}
