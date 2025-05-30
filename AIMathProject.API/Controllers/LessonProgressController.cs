using AIMathProject.Application.Command.LessonProgress;
using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Application.Queries.LessonProgress;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AIMathProject.API.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]

    public class LessonProgressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonProgressController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Returns all information of the study program the user has registered for.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API retrieves the user's study program information, including lesson details and learning progress.
        /// 
        /// **Request:**
        /// The request should include userID.
        /// 
        /// **Response:**
        /// The response will return a list of lessons, each containing:
        /// - **lessonId**: The unique identifier of the lesson.
        /// - **learningProgress**: The progress of the user in the lesson (e.g., percentage completed).
        /// - **isCompleted**: A boolean indicating whether the lesson is completed.
        /// - **lesson**: An object containing lesson details:
        ///   - **lessonOrder**: The order of the lesson in the study plan.
        ///   - **lessonName**: The name of the lesson.
        ///   - **lessonContent**: The content of the lesson.
        ///   
        ///  **Example Request:**
        /// ```http
        /// GET /api/lessonprogress/id/14
        /// </remarks>
        /// <returns>Returns all information of the study program the user has registered for.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LessonProgressDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLessonProgressById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetAllInfoProgressQuery(id)));
        }


        /// <summary>
        /// Returns lesson progress information filtered by chapter's grade and semester.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API (including user and admin)*
        /// This API retrieves lesson progress information for a specific enrollment, filtered by the chapter's grade and semester.
        /// 
        /// **Request:**
        /// The request should include the **enrollment ID**, **grade**, and **semester**.
        /// 
        /// **Response:**
        /// The response will return a list of lesson progress records matching the specified grade and semester, each containing:
        /// - **learningProgressId**: The unique identifier of the progress record.
        /// - **lessonId**: The unique identifier of the lesson.
        /// - **status**: The current status of the lesson (not_started, in_progress, completed).
        /// - **lesson**: An object containing lesson details:
        ///   - **lessonOrder**: The order of the lesson in the study plan.
        ///   - **lessonName**: The name of the lesson.
        ///   - **lessonVideoUrl**: URL to the lesson's video content.
        ///   - **lessonPdfUrl**: URL to the lesson's PDF content.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /api/lessonprogress/id/14/grade/3/semester/1
        /// </remarks>
        /// <returns>Returns filtered lesson progress information based on chapter's grade and semester.</returns> 
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("id/{id:int}/grade/{grade:int}/semester/{semester:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LessonProgressDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLessonProgressByIdClassified([FromRoute] int id,[FromRoute] int grade, [FromRoute] int semester)
        {
            return Ok(await _mediator.Send(new GetAllInfLessonProgressClassifiedQuery(id,grade, semester)));
        }

        /// <summary>
        /// Updates the learning progress of a specific lesson progress record.
        /// </summary>
        /// <remarks>
        /// *User and admin can use this api*
        /// This API updates the user's learning progress for a specific lesson.
        /// 
        /// **Request:**
        /// The request should include:
        /// - **lessonID** (int): The unique identifier of the lesson .
        /// - **enrollmentID** (int): The unique identifier of the enrollment.
        /// - **process** (int): The updated progress of the user in the lesson (e.g., 20,30,70).
        ///
        /// **Response:**
        /// If successful, the response will return the updated lesson progress information:
        /// - **learningProgressId**: The unique identifier of the lesson progress.
        /// - **lessonId**: The unique identifier of the lesson.
        /// - **process**: The updated progress of the user in the lesson (e.g., 20,30,70).
        /// - **lesson**: An object containing lesson details:
        ///   - **lessonOrder**: The order of the lesson in the study plan.
        ///   - **lessonName**: The name of the lesson.
        ///   - **lessonContent**: A link to the lesson content.
        ///
        /// **Example Request:**
        /// ```http
        /// PATCH /update/lesson/25/enrollment/2/process/20
        /// </remarks>
        /// <returns>Returns the updated learning progress information.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPatch("update/lesson/{lessonId:int}/enrollment/{enrollmentId:int}/process/{process:int}")]
        public async Task<IActionResult> UpdateLearningProgress([FromRoute] int lessonId, [FromRoute] int enrollmentId, [FromRoute] int process)
        {
            return Ok(await _mediator.Send(new UpdateLearningProgressCommand(lessonId, enrollmentId, process)));
        }
    }
}