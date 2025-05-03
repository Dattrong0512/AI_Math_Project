using AIMathProject.Application.Command.Enrollment;
using AIMathProject.Application.Command.LessonProgress;
using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Queries.Enrollment;
using AIMathProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AIMathProject.API.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EnrollmentController : ControllerBase
    {
        IMediator _mediator;

        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all enrollment information for a specific user.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API retrieves enrollment records for a given user, including details such as grade, semester, and academic year.
        ///
        /// **Request:**
        /// The request should include:
        /// - **id** (int): The unique identifier of the user.
        ///
        /// **Response:**
        /// The response will return a list of enrollment records, each containing:
        /// - **enrollmentId**: The unique identifier of the enrollment.
        /// - **userId**: The unique identifier of the user.
        /// - **grade**: The grade level associated with the enrollment.
        /// - **enrollmentDate**: The date when the user enrolled.
        /// - **avgScore**: The average score of the user in this enrollment (nullable).
        /// - **semester**: The semester in which the user is enrolled.
        /// - **startYear**: The academic start year.
        /// - **endYear**: The academic end year.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /getinfo/id/10
        /// </remarks>
        /// <returns>Returns all enrollment records for the specified user.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnrollmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInfoEnrollmentById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetEnrollmentByIdQuery(id)));

        }


        /// <summary>
        /// Retrieves the current (most recent) enrollment for a specific user.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API retrieves the most recent enrollment record for a given user based on enrollment date.
        ///
        /// **Request:**
        /// The request should include:
        /// - **userId** (int): The unique identifier of the user.
        ///
        /// **Response:**
        /// The response will return the most recent enrollment record containing:
        /// - **enrollmentId**: The unique identifier of the enrollment.
        /// - **userId**: The unique identifier of the user.
        /// - **grade**: The grade level associated with the enrollment.
        /// - **enrollmentDate**: The date when the user enrolled.
        /// - **avgScore**: The average score of the user in this enrollment (nullable).
        /// - **semester**: The semester in which the user is enrolled.
        /// - **startYear**: The academic start year.
        /// - **endYear**: The academic end year.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /api/enrollment/current/10
        /// ```
        /// </remarks>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Returns the most recent enrollment record for the specified user.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("current/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentEnrollmentByUserId([FromRoute] int userId)
        {
            var enrollment = await _mediator.Send(new GetCurrentEnrollmentByUserIdQuery(userId));

            if (enrollment == null)
            {
                return NotFound($"No enrollment records found for user ID: {userId}");
            }

            return Ok(enrollment);
        }

        /// <summary>
        /// Updates an entire enrollment record.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API (including user and admin)*
        /// This API updates all fields of a specific enrollment record.
        /// 
        /// **Request:**
        /// The request should include the full `EnrollmentDto` object.
        /// 
        /// **Response:**
        /// The response will return a list of enrollment records, each containing:
        /// - **enrollmentId**: The unique identifier of the enrollment.
        /// - **userId**: The unique identifier of the user.
        /// - **grade**: The grade level associated with the enrollment.
        /// - **enrollmentDate**: The date when the user enrolled.
        /// - **avgScore**: The average score of the user in this enrollment (nullable).
        /// - **semester**: The semester in which the user is enrolled.
        /// - **startYear**: The academic start year.
        /// - **endYear**: The academic end year.
        /// 
        /// **Example Request:**
        /// ```http
        /// PUT /api/enrollment/update
        /// Content-Type: application/json
        /// {
        ///   "enrollmentId": 12,
        ///   "userId": 10,
        ///   "grade": 2,
        ///   "enrollmentDate": "2024-08-22",
        ///   "avgScore": 8.5,
        ///   "semester": 2,
        ///   "startYear": 2024,
        ///   "endYear": 2025
        /// }
        /// </remarks>
        /// <param name="enrollmentDto">The updated enrollment object.</param>
        /// <returns>Returns the updated enrollment record.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEnrollment([FromBody] EnrollmentDto enrollmentDto)
        {
            var updatedEnrollment = await _mediator.Send(new UpdateEnrollmentCommand(enrollmentDto));

            if (updatedEnrollment == null)
            {
                return NotFound("Enrollment not found.");
            }

            return Ok(updatedEnrollment);
        }

        /// <summary>
        /// Creates a new enrollment record.
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this API (including user and admin)*
        /// This API creates a new enrollment record for a user. If avgScore is not provided, it will default to 0.
        /// 
        /// **Request:**
        /// The request should include the `EnrollmentDto` object with required fields.
        /// 
        /// **Response:**
        /// The response will return the newly created enrollment record containing:
        /// - **enrollmentId**: The unique identifier of the new enrollment.
        /// - **userId**: The unique identifier of the user.
        /// - **grade**: The grade level associated with the enrollment.
        /// - **enrollmentDate**: The date when the user enrolled.
        /// - **avgScore**: The average score of the user in this enrollment (defaults to 0 if not provided).
        /// - **semester**: The semester in which the user is enrolled.
        /// - **startYear**: The academic start year.
        /// - **endYear**: The academic end year.
        /// 
        /// **Example Request:**
        /// ```http
        /// POST /api/enrollment/create
        /// Content-Type: application/json
        /// {
        ///   "userId": 10,
        ///   "semester": 2,
        ///   "grade": 3,
        ///   "startYear": 2024,
        ///   "endYear": 2025
        /// }
        /// ```
        /// </remarks>
        /// <param name="enrollmentDto">The enrollment object to create.</param>
        /// <returns>Returns the newly created enrollment record.</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EnrollmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto == null || !enrollmentDto.UserId.HasValue)
            {
                return BadRequest("Invalid enrollment data. User ID is required.");
            }
            if (!enrollmentDto.AvgScore.HasValue)
            {
                enrollmentDto.AvgScore = 0;
            }
            var createdEnrollment = await _mediator.Send(new CreateEnrollmentCommand(enrollmentDto));

            if (createdEnrollment == null)
            {
                return BadRequest("Failed to create enrollment.");
            }

            return CreatedAtAction(nameof(GetInfoEnrollmentById), new { id = createdEnrollment.UserId }, createdEnrollment);
        }
    }
}

