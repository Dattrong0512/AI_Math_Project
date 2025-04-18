﻿using AIMathProject.Application.Command.LessonProgress;
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
        ///  **Example Request:**
        /// ```http
        /// GET /api/lessionprogress/id/6
        /// </remarks>

        /// ```
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
        /// Returns all information of the study program classified by the semester the user has registered for.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API retrieves the user's study program information, including lesson details and learning progress.
        /// 
        /// **Request:**
        /// The request should include **userID** and **Semester**.
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
        ///   **Example Request:**
        /// ```http
        /// GET /api/lessionprogress/id/6/semester/1
        /// </remarks>

        /// <returns>Returns all information of the study program classified by the semester the user has registered for.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("id/{id:int}/semester/{semester:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LessonProgressDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLessionProgressById([FromRoute] int id, [FromRoute] int semester)
        {
            return Ok(await _mediator.Send(new GetAllInfLessonProgressClassifiedQuery(id, semester)));
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
        /// - **idProgress** (int): The unique identifier of the lesson progress.
        /// - **status** (int): The status to be updated (0 is not_started, 1 is completed, 2 is in_progress).
        ///
        /// **Response:**
        /// If successful, the response will return the updated lesson progress information:
        /// - **learningProgressId**: The unique identifier of the lesson progress.
        /// - **lessonId**: The unique identifier of the lesson.
        /// - **status**: The updated progress of the user in the lesson (e.g., completed).
        /// - **lesson**: An object containing lesson details:
        ///   - **lessonOrder**: The order of the lesson in the study plan.
        ///   - **lessonName**: The name of the lesson.
        ///   - **lessonContent**: A link to the lesson content.
        ///
        /// **Example Request:**
        /// ```http
        /// PATCH /update/lessonprogressID/2/status/1
        /// </remarks>
        /// <returns>Returns the updated learning progress information.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPatch("update/lessonprogressID/{idProgress:int}/status/{status:int}")]
        public async Task<IActionResult> UpdateLearningProgress([FromRoute] int idProgress, [FromRoute] int status)
        {
            string str_status = "";
            if(status == 0){
                str_status = "not_started";
            } else if (status == 1) {
                str_status = "completed";
            } else if (status == 2){
                str_status = "in_progress";
            } else { return BadRequest("Failed to update lesson progress, please input a valid status."); }
            return Ok(await _mediator.Send(new UpdateLearningProgressCommand(idProgress, str_status)));
        }
    }
}