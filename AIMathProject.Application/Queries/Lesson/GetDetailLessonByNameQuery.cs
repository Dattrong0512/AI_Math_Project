using AIMathProject.Application.Dto.LessonDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Lesson
{
    public record GetDetailLessonByNameQuery(int grade, string lessonName) : IRequest<ICollection<LessonDto>>;
    public class GetDetailLessonByNameQueryHandler(ILessonRepository<LessonDto> repository) : IRequestHandler<GetDetailLessonByNameQuery, ICollection<LessonDto>>
    {
        public Task<ICollection<LessonDto>> Handle(GetDetailLessonByNameQuery request, CancellationToken cancellationToken)
        {
            return repository.GetDetailLessonByName(request.grade, request.lessonName);
        }
    }
}
