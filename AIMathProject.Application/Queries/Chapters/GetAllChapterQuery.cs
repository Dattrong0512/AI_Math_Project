using AIMathProject.Application.Dto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Queries.Chapters
{
    public record GetAllChaptersQuery : IRequest<ICollection<ChapterDto>>;
    public class GetAllChaptersHandler(IChapterRepository<ChapterDto> chapterRepository)
        : IRequestHandler<GetAllChaptersQuery, ICollection<ChapterDto>>
    {

        public Task<ICollection<ChapterDto>> Handle(GetAllChaptersQuery request, CancellationToken cancellationToken)
        {
            return chapterRepository.GetAllChapters();
        }
    }

}
