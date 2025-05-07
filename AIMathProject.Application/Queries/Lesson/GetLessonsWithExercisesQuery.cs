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
    public record GetLessonsWithExercisesQuery(int grade) : IRequest<List<LessonWithChapterAndExerciseDto>>;

    public class GetLessonsWithExercisesQueryHandler : IRequestHandler<GetLessonsWithExercisesQuery, List<LessonWithChapterAndExerciseDto>>
    {
        private readonly ILessonWithExerciseRepository<LessonWithChapterAndExerciseDto> _lessonRepository;

        public GetLessonsWithExercisesQueryHandler(ILessonWithExerciseRepository<LessonWithChapterAndExerciseDto> lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<List<LessonWithChapterAndExerciseDto>> Handle(GetLessonsWithExercisesQuery request, CancellationToken cancellationToken)
        {
            return await _lessonRepository.GetLessonsWithExercises(request.grade);
        }
    }
}