using AIMathProject.Application.Dto.LessonDto;
using AIMathProject.Application.Dto.ExerciseResultDto;
using AIMathProject.Application.Queries.ExerciseResult;
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
    public class ExerciseResultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the exercise result details associated with a specific enrollment ID and lesson order.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API returns the detailed information about an exercise result, including the questions and answers.
        /// 
        /// **Request Parameters:**
        /// - **id** (int): The enrollment ID of the user.
        /// - **lessonorder** (int): The sequential order of the lesson.
        /// 
        /// **Response Format:**
        /// The response will return the exercise result details, including:
        /// - **exerciseId** (int): The unique identifier of the exercise.
        /// - **enrollmentId** (int): The ID of the enrollment.
        /// - **score** (decimal, nullable): The score achieved for this exercise.
        /// - **doneAt** (datetime, nullable): The timestamp when the exercise was completed.
        /// - **lesson** (object, nullable): Information about the associated lesson.
        /// - **exerciseDetailResults** (array): List of exercise detail results, each containing:
        ///   - **questionId** (int, nullable): The ID of the question.
        ///   - **isCorrect** (bool): Whether the answer is correct or not.
        ///   - **exerciseDetailId** (int): The ID of the exercise detail.
        ///   - **exerciseResultId** (int): The ID of the exercise result.
        ///   - **exerciseDetail** (object): Details about the exercise, including:
        ///     - **exerciseId** (int): The ID of the exercise.
        ///     - **questionId** (int): The ID of the question.
        ///     - **question** (object): The complete question information, including:
        ///       - **questionId** (int): The unique identifier of the question.
        ///       - **questionType** (string): The type of question (e.g., "multiple_choice").
        ///       - **difficulty** (int): The difficulty level of the question.
        ///       - **lessonId** (int): The ID of the associated lesson.
        ///       - **imgUrl** (string): The URL of an image related to the question.
        ///       - **questionContent** (string): The text content of the question.
        ///       - **pdfSolution** (string, nullable): A PDF solution reference, if available.
        ///       - **choiceAnswers** (array): List of multiple-choice answers:
        ///         - **answerId** (int): The unique identifier of the answer choice.
        ///         - **content** (string): The text of the answer choice.
        ///         - **isCorrect** (bool): Whether this choice is the correct answer.
        ///         - **imgUrl** (string, nullable): An optional image URL for the answer choice.
        ///       - **fillAnswers** (array): List of fill-in-the-blank answers (if applicable).
        ///       - **matchingAnswers** (array): List of matching question pairs (if applicable).
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/exerciseresult/enrollment/id/11/lessonorder/1
        /// ```
        /// </remarks>
        /// <returns>Returns the exercise result details for the specified enrollment ID and lesson order.</returns>

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("exerciseresult/enrollment/id/{id:int}/lessonorder/{lessonorder:int}")]
        public async Task<ActionResult<LessonDto>> GetExerciseResultByID([FromRoute] int id, [FromRoute] int lessonorder)
        {
            return Ok(await _mediator.Send(new GetDetailExerciseResultByIdQuery(id, lessonorder)));
        }

        /// <summary>
        /// Retrieves all exercise results associated with a specific enrollment ID.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API (including user and admin)*
        /// This API returns a list of exercise results for a given enrollment ID, including details about each exercise result.
        ///
        /// **Request Parameters:**
        /// - **id** (int): The enrollment ID of the user.
        ///
        /// **Response Format:**
        /// The response will return a list of exercise results, where each result includes:
        /// - **exerciseId** (int): The unique identifier of the exercise.
        /// - **enrollmentId** (int): The ID of the enrollment.
        /// - **score** (decimal, nullable): The score achieved for this exercise.
        /// - **doneAt** (datetime, nullable): The timestamp when the exercise was completed.
        /// - **lesson** (object, nullable): Information about the associated lesson.
        /// - **exerciseDetailResults** (array): List of exercise detail results, each containing:
        ///   - **questionId** (int, nullable): The ID of the question.
        ///   - **isCorrect** (bool): Whether the answer is correct or not.
        ///   - **exerciseDetailId** (int): The ID of the exercise detail.
        ///   - **exerciseResultId** (int): The ID of the exercise result.
        ///   - **exerciseDetail** (object): Details about the exercise, including:
        ///     - **exerciseId** (int): The ID of the exercise.
        ///     - **questionId** (int): The ID of the question.
        ///     - **question** (object): The complete question information, including:
        ///       - **questionId** (int): The unique identifier of the question.
        ///       - **questionType** (string): The type of question (e.g., "multiple_choice").
        ///       - **difficulty** (int): The difficulty level of the question.
        ///       - **lessonId** (int): The ID of the associated lesson.
        ///       - **imgUrl** (string): The URL of an image related to the question.
        ///       - **questionContent** (string): The text content of the question.
        ///       - **pdfSolution** (string, nullable): A PDF solution reference, if available.
        ///       - **choiceAnswers** (array): List of multiple-choice answers:
        ///         - **answerId** (int): The unique identifier of the answer choice.
        ///         - **content** (string): The text of the answer choice.
        ///         - **isCorrect** (bool): Whether this choice is the correct answer.
        ///         - **imgUrl** (string, nullable): An optional image URL for the answer choice.
        ///       - **fillAnswers** (array): List of fill-in-the-blank answers (if applicable).
        ///       - **matchingAnswers** (array): List of matching question pairs (if applicable).
        ///
        /// **Example Request:**
        /// GET /api/exerciseresults/enrollment/id/11 
        /// </remarks>
        /// <param name="id">The enrollment ID of the user.</param>
        /// <returns>Returns a list of exercise results for the specified enrollment ID.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("exerciseresults/enrollment/id/{id:int}")]
        public async Task<ActionResult<ExerciseResultDto>> GetExerciseResultById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetAllExerciseResultsByEnrollmentIdQuery(id));
            if (result == null)
                return NotFound("No detailed exercise result found for the given parameters.");
            return Ok(result);
        }
    }
}
