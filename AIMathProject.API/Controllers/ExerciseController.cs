using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using AIMathProject.Application.Queries.Exercise;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using AIMathProject.Application.Dto;
using Azure.Core;
using AIMathProject.Application.Dto.ExerciseDto;

namespace AIMathProject.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ExerciseController : ControllerBase
    {
        IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all locked exercises that have been unlocked for a specific enrollment.
        /// </summary>
        /// <remarks>
        /// *Only logged-in users (including user and admin) can access this API.*  
        /// This API returns a list of premium exercises (IsLocked=true) that have been specifically unlocked for the given enrollment.
        ///
        /// **Request Parameters:**  
        /// - **id** (int): The enrollment ID of the user.
        /// - **grade** (int): The grade level to filter exercises.
        ///
        /// **Response Format:**  
        /// The response will return a list of unlocked exercises, where each exercise includes:
        /// - **exerciseName** (string): The name of the exercise.
        /// - **exerciseId** (int): The unique identifier of the exercise.
        /// - **isLocked** (boolean): Will always be true, as these are unlocked premium exercises.
        /// - **description** (string): The description of the exercise.
        /// - **exerciseDetails** (array): List of exercise details, each containing:
        ///   - **question** (object): Complete question information, including:
        ///     - **questionId** (int): The unique identifier of the question.
        ///     - **questionType** (string): The type of question (e.g., "multiple_choice", "fill_in_blank", "matching").
        ///     - **difficulty** (int): The difficulty level of the question.
        ///     - **lessonId** (int): The ID of the associated lesson.
        ///     - **imgUrl** (string): The URL of an image related to the question.
        ///     - **questionContent** (string): The text content of the question.
        ///     - **pdfSolution** (string, nullable): A PDF solution reference, if available.
        ///     - **choiceAnswers** (array): List of multiple-choice answers (for "multiple_choice" questions).
        ///     - **fillAnswers** (array): List of fill-in answers (for "fill_in_blank" questions).
        ///     - **matchingAnswers** (array): List of matching answers (for "matching" questions).
        ///
        /// **Example Request:**  
        /// 
        /// GET /api/exercise/detail/enrollment/id/35/grade/1
        ///
        /// **Response Codes:**  
        /// - **200 OK**: Successfully retrieved the unlocked exercises.
        /// - **400 Bad Request**: No unlocked exercises found for this enrollment.
        /// </remarks>
        /// <param name="id">The enrollment ID of the user.</param>
        /// <param name="grade">The grade level to filter exercises.</param>
        /// <returns>Returns a list of locked exercises that have been unlocked for the specified enrollment.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("exercise/detail/enrollment/id/{id:int}/grade/{grade:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUnlockExerciseDetail([FromRoute] int id, [FromRoute] int grade)
        {
            var result = await _mediator.Send(new GetExercisesWithResultsByEnrollmentIdQuery(id, grade));
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("There's no exercises !!!!.");
        }

        /// <summary>
        /// Retrieves all exercises with chapter information associated with a specific enrollment ID.
        /// </summary>
        /// <remarks>
        /// *Only logged-in users (including user and admin) can access this API, and this API is for AI support to suggest exercises user needs to improve*  
        /// This API returns a list of exercises with chapter information for a given enrollment ID.
        ///
        /// **Request Parameters:**  
        /// - **id** (int): The enrollment ID of the user.
        ///
        /// **Response Format:**  
        /// The response will return an array of exercises with the following structure:
        /// - **exerciseId** (int): The unique identifier of the exercise.
        /// - **exerciseName** (string): The name of the exercise.
        /// - **lesson** (object): Information about the lesson:
        ///   - **lessonName** (string): The name of the lesson.
        ///   - **chapter** (object): Information about the chapter:
        ///     - **chapterId** (int): The unique identifier of the chapter.
        ///     - **grade** (short): The grade level.
        ///     - **chapterOrder** (short): The order of the chapter.
        ///     - **chapterName** (string): The name of the chapter.
        ///     - **semester** (short): The semester number.
        /// - **exerciseResults** (array): List of exercise results:
        ///   - **score** (decimal): The score achieved for this exercise.
        ///   - **exerciseDetailResults** (array): List of detail results, each containing:
        ///     - **isCorrect** (boolean): Whether the answer was correct.
        ///     - **question** (object): Information about the question:
        ///       - **difficulty** (short): The difficulty level of the question.
        ///       - **imgUrl** (string, nullable): The URL of an image related to the question.
        ///       - **questionContent** (string): The text content of the question.
        ///
        /// **Example Response:**  
        /// ```json
        /// [
        ///   {
        ///     "exerciseId": 1,
        ///     "exerciseName": "Bài tập Vị trí",
        ///     "lesson": {
        ///       "lessonName": "Vị trí",
        ///       "chapter": {
        ///         "chapterId": 1,
        ///         "grade": 1,
        ///         "chapterOrder": 1,
        ///         "chapterName": "Làm quen với một số hình",
        ///         "semester": 1
        ///       }
        ///     },
        ///     "exerciseResults": [
        ///       {
        ///         "score": 7.5,
        ///         "exerciseDetailResults": [
        ///           {
        ///             "isCorrect": true,
        ///             "question": {
        ///               "difficulty": 1,
        ///               "imgUrl": "https://example.com/image.png",
        ///               "questionContent": "Đây là biển báo cấm rẽ bên nào"
        ///             }
        ///           }
        ///         ]
        ///       }
        ///     ]
        ///   }
        /// ]
        /// ```
        ///
        /// **Example Request:**  
        /// 
        /// GET /api/exercise/with-chapter/enrollment/id/8
        ///
        /// **Response Codes:**  
        /// - **200 OK**: Successfully retrieved the exercises with chapter information.  
        /// - **400 Bad Request**: No exercises found.
        /// </remarks>
        /// <param name="id">The enrollment ID of the user.</param>
        /// <returns>Returns a list of exercises with chapter information for the specified enrollment ID.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("exercise/enrollment/id/{id:int}/chapter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ExerciseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExercisesWithChapterInfo([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetExercisesWithChapterInfoByEnrollmentIdQuery(id));
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("There are no exercises available.");
        }
    }
}

