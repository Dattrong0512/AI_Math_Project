using AI_Math_Project.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AI_Math_Project.Repository;
using AI_Math_Project.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using AI_Math_Project.DTO;
namespace AI_Math_Project.Controller
{
    [Route("api/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        IChapterRepository _chapter;
        public ChapterController(IChapterRepository chapter)
        {
            _chapter = chapter;
           
        }
        /// <summary>
        ///  Return all chapters of all class
        /// </summary>
        /// /// <remarks>
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **chapterName**: The name of the chapter.
        /// - **lessons**: Null, because this api just return information about grade and chapters, not include lessions in chapter
        /// </remarks>
        /// <returns>return list of chapters of classes </returns>
        [HttpGet("chapters")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllChapter()
        {
            return Ok(await _chapter.GetAllChapters());
        }
        /// <summary>
        ///  Returns detailed information of each lesson in the chapters
        /// </summary>
        /// <remarks>
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **chapterName**: The name of the chapter.
        /// - **lessons**: A list of lessons within the chapter. Each lesson includes:
        ///   - **lessonOrder**: The order of the lesson within the chapter.
        ///   - **lessonName**: The name of the lesson.
        /// </remarks>
        /// <returns>return detail list of chapters of classes </returns>
        [HttpGet("chapters/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailChapters()
        {
            return Ok(await _chapter.GetAllDetailChapters());
        }
        /// <summary>
        ///  Returns detailed information of each lesson in the chapters classified by class
        /// </summary>
        /// <remarks>
        /// - **grade**: The grade level
        /// - **chapterOrder**: The order of the chapter in the curriculum.
        /// - **chapterName**: The name of the chapter.
        /// - **lessons**: A list of lessons within the chapter. Each lesson includes:
        ///   - **lessonOrder**: The order of the lesson within the chapter.
        ///   - **lessonName**: The name of the lesson.
        /// </remarks>
        /// <returns>return detail list of chapters of classes classified by class</returns>
        [HttpGet("chapters/grade/{grade:int}/details/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailChaptersClassifed([FromRoute] int grade)
        {
            return Ok(await _chapter.GetAllDetailChaptersClassified(grade));
        }
    }
}
