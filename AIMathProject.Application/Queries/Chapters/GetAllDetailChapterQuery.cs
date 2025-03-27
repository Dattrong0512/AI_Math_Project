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
    public record GetAllDetailChaptersQuery : IRequest<ICollection<ChapterDto>>;
    public class GetAllDetailChapterHandler(IChapterRepository<ChapterDto> chapterRepository) : IRequestHandler<GetAllDetailChaptersQuery, ICollection<ChapterDto>>
    {
        public Task<ICollection<ChapterDto>> Handle(GetAllDetailChaptersQuery request, CancellationToken cancellationToken)
        {
            return chapterRepository.GetAllDetailChapters();
        }
    }
}
