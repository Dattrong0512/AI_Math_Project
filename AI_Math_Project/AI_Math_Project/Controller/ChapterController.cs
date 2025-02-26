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
    [Route("api/[controller]")]
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
        /// <returns>return list of chapters of classes </returns>
        [HttpGet("chapters/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ChapterDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailChapters()
        {
            return Ok(await _chapter.GetAllDetailChapters());
        }
    }
}
