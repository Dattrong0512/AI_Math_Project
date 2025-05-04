using AIMathProject.Application.Dto.ExerciseWithChapterDto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Exercise
{
    public record GetExercisesWithChapterInfoByEnrollmentIdQuery(int EnrollmentId) : IRequest<List<ExerciseWithChapterDto>>;

    public class GetExercisesWithChapterInfoByEnrollmentIdQueryHandler : IRequestHandler<GetExercisesWithChapterInfoByEnrollmentIdQuery, List<ExerciseWithChapterDto>>
    {
        private readonly IExerciseSummaryRepository<ExerciseWithChapterDto> _exerciseRepository;

        public GetExercisesWithChapterInfoByEnrollmentIdQueryHandler(IExerciseSummaryRepository<ExerciseWithChapterDto> exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<List<ExerciseWithChapterDto>> Handle(GetExercisesWithChapterInfoByEnrollmentIdQuery request, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.GetExercisesWithChapterInfoByEnrollmentId(request.EnrollmentId);
        }
    }
}