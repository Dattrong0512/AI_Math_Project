using AIMathProject.Application.Dto.EnrollmentUnlockExerciseDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.EnrollmentUnlocExercise
{
    public record UnlockExerciseCommand(int ExerciseId, int EnrollmentId) : IRequest<(bool success, string message)>;

    public class UnlockExerciseCommandHandler : IRequestHandler<UnlockExerciseCommand, (bool success, string message)>
    {
        private readonly IEnrollmentUnlockExerciseRepository<EnrollmentUnlockExerciseDto> _repository;

        public UnlockExerciseCommandHandler(IEnrollmentUnlockExerciseRepository<EnrollmentUnlockExerciseDto> repository)
        {
            _repository = repository;
        }

        public Task<(bool success, string message)> Handle(UnlockExerciseCommand request, CancellationToken cancellationToken)
        {
            return _repository.UnlockExercise(request.ExerciseId, request.EnrollmentId);
        }
    }
}
