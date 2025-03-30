using AIMathProject.Application.Dto.LoginDto;
using AIMathProject.Application.Dto.Pagination;
using AIMathProject.Application.Queries.Users;
using AIMathProject.Domain.Entities;
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
        [Authorize(Policy = "Admin")]
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
        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfoUserLogin()
        {
            return Ok(await _mediator.Send(new GetInfoUserLoginQuery()));
        }
    }
}
