using AI_Math_Project.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AI_Math_Project.Controller
{
    [Route("api/question")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _repo;

        public QuestionController(IQuestionRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Retrieves all questions associated with a specific grade and lesson order.
        /// </summary>
        /// <remarks>
        /// This API returns a list of questions for a given study program, filtered by grade level and lesson order.
        ///
        /// **Request Parameters:**
        /// - **grade** (int): The grade level of the study program.
        /// - **lessionOrder** (int): The sequential order of the lesson.
        ///
        /// **Response Format:**
        /// The response will return a list of questions, each containing:
        /// - **questionId** (int): The unique identifier of the question.
        /// - **questionType** (string): The type of question (e.g., "multiple_choice", "fill_in_blank", "matching").
        /// - **difficulty** (int): The difficulty level of the question.
        /// - **lessonId** (int): The identifier of the associated lesson.
        /// - **imgUrl** (string, nullable): The URL of an image related to the question.
        /// - **questionContent** (string): The text content of the question.
        /// - **pdfSolution** (string, nullable): A PDF solution reference, if available.
        /// - **choiceAnswers** (array): List of multiple-choice answers (if applicable):
        ///   - **answerId** (int): The unique identifier of the answer choice.
        ///   - **content** (string, nullable): The text of the answer choice (if applicable).
        ///   - **isCorrect** (bool): Whether this choice is the correct answer.
        ///   - **imgUrl** (string, nullable): An optional image URL for the answer choice.
        /// - **fillAnswers** (array): List of correct answers for fill-in-the-blank questions (if applicable):
        ///   - **answerId** (int): The unique identifier of the correct answer.
        ///   - **correctAnswer** (string): The correct text answer that should be filled in.
        ///   - **position** (short): The position of the blank in the question (for multi-blank questions).
        /// - **matchingAnswers** (array): List of matching question pairs (if applicable):
        ///   - **answerId** (int): The unique identifier of the matching answer.
        ///   - **correctAnswer** (string): The correct match for the question.
        ///   - **imgUrl** (string, nullable): An image URL representing the matching item.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /api/question/grade/1/lessionorder/1
        /// ```
        /// </remarks>
        /// <returns>Returns a list of questions matching the specified grade and lesson order.</returns>
        [HttpGet("/grade/{grade:int}/lessionorder/{lessionorder:int}")]
        public async Task<IActionResult> GetQuestionByGrade([FromRoute] int grade, int lessionorder)
        {
            return Ok(await _repo.GetAllQuestionByLessionID(grade, lessionorder));
        }

    }
}
