using AIMathProject.Application.Dto.ExerciseResultDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.ExerciseResult
{
    public record GetAllExerciseResultsByEnrollmentIdQuery(int EnrollmentId) : IRequest<List<ExerciseResultDto>>;

    public class GetAllExerciseResultsByEnrollmentIdHandler : IRequestHandler<GetAllExerciseResultsByEnrollmentIdQuery, List<ExerciseResultDto>>
    {
        private readonly IExerciseResultRepository<ExerciseResultDto> _repository;

        public GetAllExerciseResultsByEnrollmentIdHandler(IExerciseResultRepository<ExerciseResultDto> repository)
        {
            _repository = repository;
        }

        public async Task<List<ExerciseResultDto>> Handle(GetAllExerciseResultsByEnrollmentIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllExerciseResultsByEnrollmentId(request.EnrollmentId);
        }
    }
}
