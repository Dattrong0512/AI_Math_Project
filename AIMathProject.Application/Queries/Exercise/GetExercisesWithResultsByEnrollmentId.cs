using AIMathProject.Application.Dto.ExerciseDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Exercise
{
    public record GetExercisesWithResultsByEnrollmentIdQuery(int EnrollmentId) : IRequest<List<ExerciseDto>>;

    public class GetExercisesWithResultsByEnrollmentIdHandler : IRequestHandler<GetExercisesWithResultsByEnrollmentIdQuery, List<ExerciseDto>>
    {
        private readonly IExerciseRepository<ExerciseDto> _repository;

        public GetExercisesWithResultsByEnrollmentIdHandler(IExerciseRepository<ExerciseDto> repository)
        {
            _repository = repository;
        }

        public async Task<List<ExerciseDto>> Handle(GetExercisesWithResultsByEnrollmentIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetExercisesWithResultsByEnrollmentId(request.EnrollmentId);
        }
    }
}
