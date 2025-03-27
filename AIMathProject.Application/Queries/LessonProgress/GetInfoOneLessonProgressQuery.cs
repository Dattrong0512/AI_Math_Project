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
    public record GetInfoOneLessonProgressQuery(int lpId) : IRequest<LessonProgressDto>;
    public class GetInfoOneLessonProgressHandler(ILessonProgressRepository<LessonProgressDto> repository)
        : IRequestHandler<GetInfoOneLessonProgressQuery, LessonProgressDto>
    {
        public Task<LessonProgressDto> Handle(GetInfoOneLessonProgressQuery request, CancellationToken cancellationToken)
        {
            return repository.GetInfoOneLessonProgress(request.lpId);
        }
    }
}
