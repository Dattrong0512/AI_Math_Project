using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Command.Login;
using AIMathProject.Application.Command.RefreshToken;
using AIMathProject.Application.Command.Register;
using AIMathProject.Application.Command.ResetPassword;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Queries.ResetPassword;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Requests;
using AIMathProject.Infrastructure.CommonServices;
using AIMathProject.Infrastructure.Options;
using AIMathProject.Infrastructure.Processors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LogoutController> _logger;
        public LogoutController(IMediator mediator, ILogger<LogoutController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Logs out the current user by invalidating tokens and ending the session.
        /// </summary>
        /// <remarks>
        /// This API handles user logout by:
        /// - Invalidating refresh tokens
        /// - Clearing authentication cookies
        /// - Recording session end time for analytics
        ///
        /// **Example Request:**
        /// ```http
        /// POST /account/logout
        /// Authorization: Bearer {your-jwt-token}
        /// ```
        /// </remarks>
        /// <returns>Returns a success message upon successful logout</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost("account/logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
                {
                    return Unauthorized("Invalid user ID");
                }
                await _mediator.Send(new LogoutCommand(userIdInt));
                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, "An error occurred during logout");
            }
        }
    }
}