using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.LessonProgress
{
    public record GetAllInfLessonProgressClassifiedQuery(int id, int grade, int semester) : IRequest<ICollection<LessonProgressDto>>;
    public class GetAllInfLessonProgressClassifiedHandler(ILessonProgressRepository<LessonProgressDto> repository)
        : IRequestHandler<GetAllInfLessonProgressClassifiedQuery, ICollection<LessonProgressDto>>
    {
        public Task<ICollection<LessonProgressDto>> Handle(GetAllInfLessonProgressClassifiedQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllInfLessonProgressClassified(request.id, request.grade ,request.semester);
        }
    }
}

