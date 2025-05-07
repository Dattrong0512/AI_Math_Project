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
        /// Retrieves all exercise results associated with a specific enrollment ID.
        /// </summary>
        /// <remarks>
        /// *Only logged-in users (including user and admin) can access this API.*  
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
        /// - **lesson** (object, nullable): Information about the associated lesson:
        ///   - **lessonOrder** (short, nullable): The order of the lesson.
        ///   - **lessonName** (string): The name of the lesson.
        ///   - **lessonVideoUrl** (string, nullable): The URL of the lesson video.
        ///   - **lessonPdfUrl** (string, nullable): The URL of the lesson PDF.
        ///   - **chapterOrder** (short, nullable): The order of the chapter.
        ///   - **questions** (array, nullable): List of questions associated with the lesson.
        /// - **exerciseDetailResults** (array): List of exercise detail results, each containing:
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
        ///
        /// **Example Request:**  
        /// 
        /// GET /api/exerciseresults/enrollment/id/8
        ///
        /// **Response Codes:**  
        /// - **200 OK**: Successfully retrieved the exercise results.  
        /// - **404 Not Found**: No exercise results found for the given enrollment ID.
        /// </remarks>
        /// <param name="id">The enrollment ID of the user.</param>
        /// <returns>Returns a list of exercise results for the specified enrollment ID.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("exercise/detail/enrollment/id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpsertExerciseDetailResult([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetExercisesWithResultsByEnrollmentIdQuery(id));
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

