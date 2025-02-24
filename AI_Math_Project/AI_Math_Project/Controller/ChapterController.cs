using AI_Math_Project.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AI_Math_Project.Repository;
using AI_Math_Project.Interfaces;
namespace AI_Math_Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        IChapterRepository _chapter;

        public ChapterController(IChapterRepository chapter)
        {
            _chapter = chapter;
        }

        [HttpGet("getAllChapter")]
        public async Task<IActionResult> GetAllChapter()
        {
            return Ok(await _chapter.GetAllChapters());
        }
        [HttpGet("getDetailChapters")]
        public async Task<IActionResult> GetDetailChapters()
        {
            return Ok(await _chapter.GetAllDetailChapters());
        }
    }
}
