using AIMathProject.Application.Command.EnrollmentUnlocExercise;
using AIMathProject.Application.Queries.Exercise;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AIMathProject.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EnrollmentUnlockExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentUnlockExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Unlocks a specific exercise for a user's enrollment by spending coins.
        /// </summary>
        /// <remarks>
        /// *Only logged-in users can access this API.*
        /// This API unlocks a specific exercise for the given enrollment by spending 1 coin from the user's wallet.
        /// 
        /// **Request Parameters:**
        /// - **enrollmentId** (int): The ID of the enrollment.
        /// - **exerciseId** (int): The ID of the exercise to unlock.
        /// 
        /// /// **Example Request:**
        /// ```http
        /// POST api/enrollment/8/exercise/401/unlock
        /// ```
        /// 
        /// **Response Codes:**
        /// - **200 OK**: Successfully unlocked the exercise.
        /// - **400 Bad Request**: Unable to unlock the exercise (see message for details).
        /// - **401 Unauthorized**: User is not authorized.
        /// </remarks>
        /// <param name="enrollmentId">The ID of the enrollment</param>
        /// <param name="exerciseId">The ID of the exercise to unlock</param>
        /// <returns>A result indicating success or failure with a message</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("enrollment/{enrollmentId:int}/exercise/{exerciseId:int}/unlock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UnlockExercise([FromRoute] int enrollmentId, [FromRoute] int exerciseId)
        {
            var result = await _mediator.Send(new UnlockExerciseCommand(exerciseId, enrollmentId));

            if (!result.success)
            {
                return BadRequest(result.message);
            }

            return Ok(result.message);
        }
    }
}