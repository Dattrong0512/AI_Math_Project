using AIMathProject.Application.Command.Lesson;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Queries.Lesson;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AIMathProject.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the details of a lesson associated with a specific grade, chapter, and lesson order.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
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
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("lesson/grade/{grade:int}/lessonorder/{lessonorder:int}")]
        public async Task<ActionResult<LessonDto>> GetLessonByID([FromRoute] int grade, [FromRoute] int lessonorder)
        {
            return Ok(await _mediator.Send(new GetDetailLessonByIdQuery(grade, lessonorder)));
        }


        /// <summary>
        /// Creates a new lesson associated with a specific grade and chapter order.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
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
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("lesson/grade/{grade:int}/chapter/{chapterorder:int}")]
        public async Task<ActionResult<LessonDto>> CreateLesson([FromRoute] int grade, [FromRoute] int chapterorder, [FromBody] LessonDto lessonDto)
        {
            bool result = await _mediator.Send(new CreateLessonCommand(grade, chapterorder, lessonDto));
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

        /// <summary>
        /// Retrieves the list of lessons for a specific grade and lesson name.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API retrieves all lessons that match a specific grade level and lesson name.
        /// The search is case-insensitive and ignores accents in the lesson name.
        /// 
        /// **Request Parameters:**
        /// - **grade** (int): The grade level of the study program.
        /// - **lessonname** (string): The name of the lesson to search for. The search will return lessons whose name contains the provided `lessonname`, regardless of case and accents.
        /// 
        /// **Response Format:**
        /// The response will return a list of lessons that match the specified criteria, including:
        /// - **lessonOrder** (short?): The order of the lesson within the chapter.
        /// - **lessonName** (string): The name of the lesson.
        /// - **lessonContent** (string, nullable): The content of the lesson, if available. This can be null if no content is provided.
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/lesson/grade/1/lessonname/vi tri
        /// ```
        /// </remarks>
        /// <returns>Returns a list of `LessonDto` objects representing the found lessons, or an empty list if no lessons are found.</returns>

        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LessonDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("lesson/grade/{grade:int}/lessonname/{lessonname}")]
        public async Task<IActionResult> GetLessonByName([FromRoute] int grade, [FromRoute] string lessonname)
        {
            return Ok(await _mediator.Send(new GetDetailLessonByNameQuery(grade, lessonname)));
        }
    }
}
