using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.AnswerDto
{
    public class UserFillAnswerDto
    {
        public string? WrongAnswer { get; set; }

        public int? Position { get; set; }

    }
}
