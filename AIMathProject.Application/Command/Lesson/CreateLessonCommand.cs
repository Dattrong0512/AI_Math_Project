using AIMathProject.Application.Dto.LessonDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Lesson
{
    public record CreateLessonCommand(int grade, int chapterorder, LessonDto lessonDto) : IRequest<bool>;

    public class CreateLessonCommandHandler(ILessonRepository<LessonDto> repository) : IRequestHandler<CreateLessonCommand, bool>
    {
        public Task<bool> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            return repository.CreateLesson(request.grade, request.chapterorder, request.lessonDto);
        }
    }
}
