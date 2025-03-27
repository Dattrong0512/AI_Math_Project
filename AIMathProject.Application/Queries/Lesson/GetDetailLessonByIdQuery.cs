using AIMathProject.Application.Dto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Lesson
{

    public record GetDetailLessonByIdQuery(int grade,int chapterOrder, int lessonOrder) : IRequest<LessonDto>;
    public class GetDetailLessonByIdQueryQueryHandler(ILessonRepository<LessonDto> repository) : IRequestHandler<GetDetailLessonByIdQuery, LessonDto>
    {
        public Task<LessonDto> Handle(GetDetailLessonByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetDetailLessonById(request.grade, request.chapterOrder,request.lessonOrder);
        }
    }
}
