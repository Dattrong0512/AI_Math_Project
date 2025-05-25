using AIMathProject.Application.Command.Users;
using AIMathProject.Application.Dto.LoginDto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Queries.Users;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Requests;
using AIMathProject.Infrastructure.Options;
using AIMathProject.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AIMathProject.API.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of user information, accessible only to users with Admin privileges.
        /// </summary>
        /// <param name="pageindex">The index of the page to retrieve (starting from 0).</param>
        /// <param name="pagesize">The number of users to include per page.</param>
        /// <returns>
        /// Returns an <see cref="OkObjectResult"/> containing a <see cref="Pagination{UserDto}"/> object with the paginated list of users.
        /// </returns>
        /// <remarks>
        /// - This endpoint requires the caller to be authenticated and have the "Admin" role.
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Admin")]
        [HttpGet("pageindex/{pageindex:int}/pagesize/{pagesize:int}")]
        public async Task<IActionResult> GetInfoUser([FromRoute] int pageindex,int pagesize)
        {
            return Ok(await _mediator.Send(new GetInfoUserQuery(pageindex, pagesize)));
        }

        /// <summary>
        /// This api used to get information of currently logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfoUserLogin()
        {
            return Ok(await _mediator.Send(new GetInfoUserLoginQuery()));
        }

        /// <summary>
        /// Gets users with advanced filtering and searching capabilities
        /// </summary>
        /// <remarks>
        /// *Only administrators can use this API*
        /// 
        /// **Functionality:**
        /// - Search users by email or username (case-insensitive matching)
        /// - Filter users by role (1 for User, 2 for Admin)
        /// - Filter users by status (true for active, false for inactive)
        /// - All filters are optional - leave empty to skip filtering
        /// - Results are paginated and ordered alphabetically by username
        ///
        /// **Request:**
        /// The request can include the following query parameters:
        /// - **searchTerm** (optional): Text to search in email and username fields
        /// - **role** (optional): Filter by user role (1 for User, 2 for Admin)
        /// - **status** (optional): Filter by account status (true for active, false for inactive)
        /// - **pageindex**: The page index (starts from 0)
        /// - **pagesize**: Number of records per page
        ///
        /// **Response:**
        /// The response will return a paginated list of users:
        /// - **pageIndex**: Current page index
        /// - **pageSize**: Number of items per page
        /// - **totalCount**: Total number of matching users
        /// - **items**: Array of user objects containing id, userName, email, phoneNumber, gender, dob, avatar, and status
        ///
        /// **Example Requests:**
        /// ```
        /// GET /api/user/filter?pageindex=0&amp;pagesize=10
        /// GET /api/user/filter?searchTerm=john&amp;role=1&amp;status=true&amp;pageindex=0&amp;pagesize=10
        /// ```
        /// </remarks>
        /// <param name="searchTerm">Text to search in email and username fields</param>
        /// <param name="role">Filter by user role (1 for User, 2 for Admin)</param>
        /// <param name="status">Filter by account status (true for active, false for inactive)</param>
        /// <param name="pageindex">The page index (starts from 0)</param>
        /// <param name="pagesize">Number of records per page</param>
        /// <returns>Returns a paginated list of users matching the search and filter criteria</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetUsersWithFilters(
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? role = null,
            [FromQuery] bool? status = null,
            [FromQuery] int pageindex = 0,
            [FromQuery] int pagesize = 10)
        {
            return Ok(await _mediator.Send(new GetUsersWithFiltersQuery(searchTerm, role, status, pageindex, pagesize)));
        }


        /// <summary>
        /// Deletes a user by their email, accessible only to users with Admin privileges.
        /// </summary>
        /// <param name="email">The email of the user to delete.</param>
        /// <remarks>
        /// - This endpoint requires the caller to be authenticated and have the "Admin" role.
        /// </remarks>
        [Authorize(Policy = "Admin")]
        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand(email));
                return Ok("User deleted successfully");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a user by their ID, accessible only to users with Admin privileges.
        /// </summary>
        /// <param name="request">The update request containing the fields to update.</param>
        /// <returns>
        /// Returns a <see cref="NoContentResult"/> if the user was successfully updated, or a <see cref="NotFoundResult"/> if the user was not found.
        /// </returns>
        /// <remarks>
        /// - This endpoint requires the caller to be authenticated and have the  role.
        /// </remarks>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("account/update/")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateRequest request)
        {
            try
            {
                await _mediator.Send(new UpdateUserCommand(request));
                return Ok("account updated successfully");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
