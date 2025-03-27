using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Question
{
    public record GetAllQuestionByLessonIDQuery(int grade, int questionOrder) : IRequest<ICollection<QuestionDto>>;
    public class GetAllQuestionByLessonIDHandler(IQuestionRepository<QuestionDto> repository) :
        IRequestHandler<GetAllQuestionByLessonIDQuery, ICollection<QuestionDto>>
    {
        public Task<ICollection<QuestionDto>> Handle(GetAllQuestionByLessonIDQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllQuestionByLessonID(request.grade, request.questionOrder);
        }
    }
}
