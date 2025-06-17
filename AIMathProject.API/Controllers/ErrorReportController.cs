using AIMathProject.Application.Command.ErrorReport;
using AIMathProject.Application.Commands.ErrorReport;
using AIMathProject.Application.Dto.ErrorReportDto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Queries.ErrorReport;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AIMathProject.API.Controllers
{
    [Route("api/errorreport")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ErrorReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ErrorReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new error report from a user
        /// </summary>
        /// <remarks>
        /// *Only logged in users can use this api (including user and admin)*
        /// This API creates a new error report with the provided error message.
        /// The error_type is automatically set to "user" and resolved is set to false (0).
        /// 
        /// **Example Request:**
        /// ```http
        /// POST /api/ErrorReport/user/123
        /// Content-Type: application/json
        /// 
        /// {
        ///     "errorMessage": "Cannot access lesson content"
        /// }
        /// ```
        /// 
        /// **Response Format:**
        /// The response will return the complete details of the created error report.
        /// </remarks>
        /// <param name="id">The user ID reporting the error</param>
        /// <param name="request">The error report request containing the error message</param>
        /// <returns>Returns the created error report</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("user/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ErrorReportDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ErrorReportDto>> CreateErrorReport(
            [FromRoute] int id,
            [FromBody] ErrorReportRequest request)
        {
            try
            {
                var command = new CreateErrorReportCommand(id, request.ErrorMessage);
                var createdReport = await _mediator.Send(command);
                return StatusCode(StatusCodes.Status201Created, createdReport);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the error report.");
            }
        }

        /// <summary>
        /// Retrieves error reports with advanced filtering, searching, and pagination capabilities.
        /// </summary>
        /// <remarks>
        /// *Only administrators can use this functionality*
        /// This method retrieves error reports based on various filter criteria with pagination support.
        /// 
        /// **Request Parameters:**
        /// - **searchTerm** (string, optional): Text to search in error message field or error ID.
        /// - **errorType** (string, optional): Filter by error type (e.g., "user", "procedure", "unknown").
        /// - **resolved** (bool?, optional): Filter by resolution status (true for resolved, false for unresolved).
        /// - **pageNumber** (int, optional): The page number for pagination (starts from 1, default: 1).
        /// - **pageSize** (int, optional): Number of records per page (default: 10).
        /// - **newestFirst** (bool, optional): Sort order (true for newest first, false for oldest first, default: true).
        /// 
        /// **Response Format:**
        /// The response will return a list of error reports matching the criteria, each containing:
        /// - **errorId** (int): The unique identifier of the error report.
        /// - **userId** (int): The ID of the user who reported the error.
        /// - **errorMessage** (string): The description of the error.
        /// - **errorType** (string): The type of error.
        /// - **createdAt** (DateTime): The timestamp when the error was reported.
        /// - **resolved** (bool): The resolution status.
        /// - **resolvedAt** (DateTime?): The timestamp when the error was resolved, if applicable.
        ///
        /// </remarks>
        /// <returns>Returns a paginated list of error reports matching the filter criteria</returns>
        [HttpGet("error/filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pagination<ErrorReportDto>>> GetErrorReportsWithFilters(
            [FromQuery] string searchTerm = null,
            [FromQuery] string errorType = null,
            [FromQuery] bool? resolved = null,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _mediator.Send(new GetErrorReportsQuery(
                    searchTerm,
                    errorType,
                    resolved,
                    pageIndex,
                    pageSize));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving error reports: {ex.Message}");
            }
        }

        /// <summary>
        /// Marks an error report as resolved.
        /// </summary>
        /// <remarks>
        /// *Only administrators can use this functionality*
        /// This method updates an error report's status to resolved and sets the resolution timestamp.
        /// 
        /// **Request Parameters:**
        /// - **errorId** (int): The unique identifier of the error report to resolve.
        /// 
        /// **Response Format:**
        /// The response will return the updated error report details including:
        /// - **errorId** (int): The unique identifier of the error report.
        /// - **userId** (int): The ID of the user who reported the error.
        /// - **errorMessage** (string): The description of the error.
        /// - **errorType** (string): The type of error.
        /// - **createdAt** (DateTime): The timestamp when the error was reported.
        /// - **resolved** (bool): The resolution status (set to true).
        /// - **resolvedAt** (DateTime): The timestamp when the error was resolved.
        /// 
        /// **Example Request:**
        /// ```http
        /// PUT /api/errorreport/resolve/error/id/111;
        /// ```
        /// </remarks>
        /// <returns>Returns the updated error report with resolution details</returns>
        [HttpPut("resolve/error/id/{id:int}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ErrorReportDto>> ResolveErrorReport([FromRoute] int id)
        {
            try
            {
                var result = await _mediator.Send(new ResolveErrorReportCommand(id));
                return Ok(result);
            }
            catch (ApplicationException ex) when (ex.Message.Contains("not found"))
            {
                return NotFound($"Error report with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while resolving error report: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an error report by its unique identifier.
        /// </summary>
        /// <remarks>
        /// *Only administrators can use this functionality*
        /// This method permanently removes an error report from the system.
        /// 
        /// **Request Parameters:**
        /// - **errorId** (int): The unique identifier of the error report to delete.
        /// 
        /// **Response Format:**
        /// The response will return a boolean value:
        /// - **true**: If the error report was successfully deleted.
        /// - **false**: If the error report with the specified ID was not found.
        /// 
        /// **Example Request:**
        /// ```http
        /// DELETE api/errorreport/delete/error/id/114;
        /// ```
        /// </remarks>
        /// <returns>Returns true if deletion was successful, false if error report was not found</returns>
        [HttpDelete("delete/error/id/{id:int}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteErrorReport([FromRoute] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteErrorReportCommand(id));

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound($"Error report with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting error report: {ex.Message}");
            }
        }
    }
}