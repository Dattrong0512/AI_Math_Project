using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.ExerciseDetailResult
{
    public record UpsertExerciseDetailResultCommand(int enrollment_id, int exercise_id, List<ExerciseDetailResultDto> edrDtoList, int completion_time) : IRequest<bool>;

    public class UpsertExerciseDetailResultCommandHandler(IExerciseDetailResultRepository<ExerciseDetailResultDto> repository) : IRequestHandler<UpsertExerciseDetailResultCommand, bool>
    {
        public Task<bool> Handle(UpsertExerciseDetailResultCommand request, CancellationToken cancellationToken)
        {
            return repository.UpsertExerciseDetailResult(request.enrollment_id, request.exercise_id, request.edrDtoList, request.completion_time);
        }
    }
}
