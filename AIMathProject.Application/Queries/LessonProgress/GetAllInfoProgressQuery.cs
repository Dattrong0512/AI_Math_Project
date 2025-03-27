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
    public record GetAllInfoProgressQuery(int id) : IRequest<ICollection<LessonProgressDto>>;
    public class GetAllInfoProgressHandler(ILessonProgressRepository<LessonProgressDto> repository)
        : IRequestHandler<GetAllInfoProgressQuery, ICollection<LessonProgressDto>>
    {
        public Task<ICollection<LessonProgressDto>> Handle(GetAllInfoProgressQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllInfLessonProgress(request.id);
        }
    }
}
