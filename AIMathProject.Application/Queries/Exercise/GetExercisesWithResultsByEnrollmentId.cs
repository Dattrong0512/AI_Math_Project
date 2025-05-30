using AIMathProject.Application.Dto.ExerciseDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Exercise
{
    public record GetExercisesWithResultsByEnrollmentIdQuery(int EnrollmentId, int Grade) : IRequest<List<ExerciseExtraForLessonDto>>;

    public class GetExercisesWithResultsByEnrollmentIdHandler : IRequestHandler<GetExercisesWithResultsByEnrollmentIdQuery, List<ExerciseExtraForLessonDto>>
    {
        private readonly IExerciseRepository<ExerciseExtraForLessonDto> _repository;

        public GetExercisesWithResultsByEnrollmentIdHandler(IExerciseRepository<ExerciseExtraForLessonDto> repository)
        {
            _repository = repository;
        }

        public async Task<List<ExerciseExtraForLessonDto>> Handle(GetExercisesWithResultsByEnrollmentIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetExercisesWithResultsByEnrollmentId(request.EnrollmentId, request.Grade);
        }
    }
}
