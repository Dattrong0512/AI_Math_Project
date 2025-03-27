using AIMathProject.Application.Dto;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Chapters
{
    public record GetAllDetailChaptersClassifiedQuery(int Grade) : IRequest<ICollection<ChapterDto>>;

    public class GetAllDetailChapterClassifiedHandler(IChapterRepository<ChapterDto> chapterRepository) :
        IRequestHandler<GetAllDetailChaptersClassifiedQuery, ICollection<ChapterDto>>
    {
        public Task<ICollection<ChapterDto>> Handle(GetAllDetailChaptersClassifiedQuery request, CancellationToken cancellationToken)
        {
            return chapterRepository.GetAllDetailChaptersClassified(request.Grade);
        }
    }
}
