using AIMathProject.Application.Dto.ExerciseDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Exercise
{
    public record GetExerciseByIdWithEnrollmentCheckQuery(int ExerciseId, int? EnrollmentId) : IRequest<ExerciseExtraForLessonDto>;

    public class GetExerciseByIdWithEnrollmentCheckQueryHandler : IRequestHandler<GetExerciseByIdWithEnrollmentCheckQuery, ExerciseExtraForLessonDto>
    {
        private readonly IExerciseRepository<ExerciseExtraForLessonDto> _exerciseRepository;

        public GetExerciseByIdWithEnrollmentCheckQueryHandler(IExerciseRepository<ExerciseExtraForLessonDto> exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<ExerciseExtraForLessonDto> Handle(GetExerciseByIdWithEnrollmentCheckQuery request, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.GetExerciseByIdWithEnrollmentCheck(request.ExerciseId, request.EnrollmentId);
        }
    }
}