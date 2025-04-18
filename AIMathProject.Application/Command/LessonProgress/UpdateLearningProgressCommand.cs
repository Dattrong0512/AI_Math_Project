using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.LessonProgress
{
    public record UpdateLearningProgressCommand(int idProgress, string status) : IRequest<LessonProgressDto>;

    public class UpdateLearningProgressHandler(ILessonProgressRepository<LessonProgressDto> repository)
        : IRequestHandler<UpdateLearningProgressCommand, LessonProgressDto>
    {
        public Task<LessonProgressDto> Handle(UpdateLearningProgressCommand request, CancellationToken cancellationToken)
        {
            return repository.UpdateLearningProgress(request.idProgress, request.status);
        }
    }
}
