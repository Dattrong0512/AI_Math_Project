using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Application.Command.ExerciseDetailResult;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using AIMathProject.Application.Dto;
using Azure.Core;

namespace AIMathProject.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ExerciseDetailResultController : ControllerBase
    {
        IMediator _mediator;

        public ExerciseDetailResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates or updates exercise detail results for a user's enrollment in a specific exercise.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API creates or updates the exercise detail results based on the user's answers.
        /// 
        /// **Request Parameters:**
        /// - **id** (int): The enrollment ID of the user.
        /// - **exerciseid** (int): The exercise id of the exercise.
        /// - **edrDtoList** (List&lt;ExerciseDetailResultDto&gt;): List of exercise detail results. Each item should include:
        ///   - **questionId** (int): The unique identifier of the question being answered.
        ///   - **isCorrect** (bool): Whether the user's answer is correct or not.
        ///   - **questionType** (string): Type of the question ("multiple_choice", "fill_in_blank", "matching").
        ///   - **choiceAnswerId** (int, optional): For multiple_choice questions, the ID of the selected choice.
        ///   - **userFillAnswers** (array, optional): For fill_in_blank questions, contains user's wrong answers:
        ///     - **wrongAnswer** (string): The incorrect answer provided by the user.
        ///     - **position** (int): The position of the blank in the question.
        /// 
        /// **Response Format:**
        /// - **200 OK**: If the exercise detail results are successfully created or updated, the response will be `true`.
        /// - **400 Bad Request**: If the operation fails, the response will contain an error message with details on the failure.
        /// 
        /// **Example Request:**
        /// ```http
        /// POST /api/exerciseresult/enrollment/id/8/exercise/id/132
        /// Content-Type: application/json
        /// [
        ///   {
        ///     "questionId": 9,
        ///     "isCorrect": true,
        ///     "questionType": "fill_in_blank"  
        ///   },
        ///   {
        ///     "questionId": 10,
        ///     "isCorrect": false,
        ///     "questionType": "fill_in_blank",
        ///     "userFillAnswers": [
        ///       {
        ///         "wrongAnswer": "750",
        ///         "position": 1
        ///       }
        ///     ]
        ///   },
        ///   {
        ///     "questionId": 11,
        ///     "isCorrect": true,
        ///     "questionType": "multiple_choice"
        ///   },
        ///   {
        ///     "questionId": 12,
        ///     "isCorrect": false,
        ///     "questionType": "multiple_choice",
        ///     "choiceAnswerId": 22
        ///   },
        ///   {
        ///     "questionId": 13,
        ///     "isCorrect": true,
        ///     "questionType": "fill_in_blank"
        ///   }
        /// ]
        /// ```
        /// 
        /// **Example Response (Success):**
        /// ```json
        /// HTTP/1.1 200 OK
        /// true
        /// ```
        /// </remarks>
        /// <returns>Returns `true` if operation succeeds, or a `BadRequest` with an error message if it fails.</returns>

        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("exerciseresult/enrollment/id/{id:int}/exercise/id/{exerciseid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpsertExerciseDetailResult([FromRoute] int id, [FromRoute] int exerciseid, [FromBody] List<ExerciseDetailResultDto> edrDtoList)
        {
            bool result = await _mediator.Send(new UpsertExerciseDetailResultCommand(id, exerciseid, edrDtoList));
            if (result)
            {
                return Ok(true);
            }
            return BadRequest("Failed to create exercise detail result.");
        }
    }
}

