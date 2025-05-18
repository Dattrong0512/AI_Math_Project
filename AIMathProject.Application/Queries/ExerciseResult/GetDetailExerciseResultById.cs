using AIMathProject.Application.Dto.ExerciseResultDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.ExerciseResult
{
    public record GetDetailExerciseResultByIdQuery(int enrollment_id, int exercise_id) : IRequest<ExerciseResultDto>;

    public class GetDetailExerciseResultByIdHandler(IExerciseResultRepository<ExerciseResultDto> repository) : IRequestHandler<GetDetailExerciseResultByIdQuery, ExerciseResultDto>
    {
        public Task<ExerciseResultDto> Handle(GetDetailExerciseResultByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetDetailExerciseResultById(request.enrollment_id, request.exercise_id);
        }
    }
}
