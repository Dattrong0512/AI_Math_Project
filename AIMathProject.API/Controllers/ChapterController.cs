using AIMathProject.Application.Dto;
using AIMathProject.Application.Queries.Chapters;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AIMathProject.API.Controllers
{
    [Route("api/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChapterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///  Return all chapters of all class
        /// </summary>
        /// /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **chapterName**: The name of the chapter.
        /// - **lessons**: Null, because this api just return information about grade and chapters, not include lessions in chapter
        /// </remarks>
        /// <returns>return list of chapters of classes </returns>
        [Authorize]
        [HttpGet("chapters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllChapter()
        {
            return Ok(await _mediator.Send(new GetAllChaptersQuery()));
        }
        /// <summary>
        ///  Returns detailed information of each lesson in the chapters
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **semester**: semester
        /// - **chapterName**: The name of the chapter.
        /// - **lessons**: A list of lessons within the chapter. Each lesson includes:
        ///   - **lessonOrder**: The order of the lesson within the chapter.
        ///   - **lessonName**: The name of the lesson.
        /// </remarks>
        /// <returns>return detail list of chapters of classes </returns>
        [Authorize]
        [HttpGet("chapters/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailChapters()
        {
            return Ok(await _mediator.Send(new GetAllDetailChaptersQuery()));
        }
        /// <summary>
        ///  Returns detailed information of each lesson in the chapters classified by class
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **chapterName**: The name of the chapter.
        /// - **semester**: semester 
        /// - **lessons**: A list of lessons within the chapter. Each lesson includes:
        ///   - **lessonOrder**: The order of the lesson within the chapter.
        ///   - **lessonName**: The name of the lesson.
        /// </remarks>
        /// <returns>return detail list of chapters of classes classified by class</returns>
        [Authorize]
        [HttpGet("chapters/grade/{grade:int}/details/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailChaptersClassifed([FromRoute] int grade)
        {
            return Ok(await _mediator.Send(new GetAllDetailChaptersClassifiedQuery(grade)));
        }

    }
}
