using AI_Math_Project.DTO;
using AI_Math_Project.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Reflection;

namespace AI_Math_Project.Controller
{
    [Route("api/")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class LessionController : ControllerBase
    {
        private readonly ILessonRepository _repo;

        public LessionController(ILessonRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Retrieves the details of a lesson associated with a specific grade, chapter, and lesson order.
        /// </summary>
        /// <remarks>
        /// This API returns the details of a lesson, filtered by grade level, chapter order, and lesson order.
        /// 
        /// **Request Parameters:**
        /// - **grade** (int): The grade level of the study program.
        /// - **chapterorder** (int): The sequential order of the chapter.
        /// - **lessonorder** (int): The sequential order of the lesson within the chapter.
        /// 
        /// **Response Format:**
        /// The response will return the details of the lesson, including:
        /// - **lessonOrder** (short?): The order of the lesson within the chapter. Nullable to allow for optional lesson orders.
        /// - **lessonName** (string): The name of the lesson.
        /// - **lessonContent** (string, nullable): The content of the lesson, if available. This can be null if no content is provided.
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/lesson/grade/1/chapterorder/1/lessonorder/1
        /// ```
        /// </remarks>
        /// <returns>Returns the lesson details matching the specified grade, chapter order, and lesson order.</returns>
        [HttpGet("lesson/grade/{grade:int}/chapterorder/{chapterorder:int}/lessonorder/{lessonorder:int}")]
        public async Task<ActionResult<LessonDto>> GetLessonByID([FromRoute] int grade, [FromRoute] int chapterorder, [FromRoute] int lessonorder)
        {
            return Ok(await _repo.GetDetailLessonById(grade,chapterorder,lessonorder));
        }


        /// <summary>
        /// Creates a new lesson associated with a specific grade and chapter order.
        /// </summary>
        /// <remarks>
        /// This API creates a new lesson and saves it in the system. The lesson is created with the provided grade level, chapter order, and lesson data.
        /// 
        /// **Request Parameters:**
        /// - **grade** (int): The grade level of the study program.
        /// - **chapterorder** (int): The sequential order of the chapter in the study program.
        /// - **lessonDto** (LessonDto): The details of the lesson to be created. This should include:
        ///   - **lessonOrder** (short?): The order of the lesson within the chapter.
        ///   - **lessonName** (string): The name of the lesson.
        ///   - **lessonContent** (string, nullable): The content of the lesson (optional).
        /// 
        /// **Response Format:**
        /// - **201 Created**: If the lesson is successfully created, the response will include the created `LessonDto` and the URL of the new lesson.
        /// - **400 Bad Request**: If the lesson could not be created, the response will contain an error message with details on the failure.
        /// 
        /// **Example Request:**
        /// ```http
        /// POST /api/lesson/grade/1/chapter/1
        /// Content-Type: application/json
        /// {
        ///   "lessonOrder": 2,
        ///   "lessonName": "Vị trí tiết 2",
        ///   "lessonContent": "linkyoutube"
        /// }
        /// **Example Response (Success):**
        /// ```json
        /// HTTP/1.1 201 Created
        /// Location: /api/lesson/grade/1/chapter/1/lessonorder/2
        /// {
        ///   "lessonOrder": 2,
        ///   "lessonName": "Vị trí tiết 2",
        ///   "lessonContent": "linkyoutube"
        /// }
        /// </remarks>
        /// <returns>Returns a `LessonDto` object representing the created lesson, or a `BadRequest` with an error message if creation fails.</returns>


        [HttpPost("lesson/grade/{grade:int}/chapter/{chapterorder:int}")]
        public async Task<ActionResult<LessonDto>> CreateLesson([FromRoute] int grade, [FromRoute] int chapterorder, [FromBody] LessonDto lessonDto)
        {
            bool result = await _repo.CreateLesson(grade, chapterorder, lessonDto);
            if (result == true)
            {
                return CreatedAtAction(nameof(GetLessonByID), new { grade = grade, chapterorder = chapterorder, lessonorder = lessonDto.LessonOrder }, lessonDto);
            }
            else
            {
                // Trả về 400 Bad Request nếu không tạo được Lesson
                return BadRequest("Unable to create lesson. Please check the provided data.");
            }

        }
    }
}
