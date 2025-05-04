using AIMathProject.Application.Dto.AnswerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.QuestionDto
{
    public class QuestionSummaryDto
    {
        public short? Difficulty { get; set; }
        public string? ImgUrl { get; set; }
        public string? QuestionContent { get; set; }
    }
}
