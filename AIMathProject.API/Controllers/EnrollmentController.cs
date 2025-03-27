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
        [Authorize]
        [HttpGet("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnrollmentDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInfoEnrollmentById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetEnrollmentByIdQuery(id)));

        }
    }
}

