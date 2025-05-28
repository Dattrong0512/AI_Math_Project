using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.AnswerDto
{
    public class UserChoiceAnswerDto
    {
        public int? AnswerId { get; set; }

        public bool? IsCorrect { get; set; }

    }
}