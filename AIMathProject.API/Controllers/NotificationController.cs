using AIMathProject.Application.Command.Notification;
using AIMathProject.Application.Queries.Notification;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AIMathProject.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves all notifications for admin users.
        /// </summary>
        /// <remarks>
        /// *Only admin users can use this API.*
        /// This API returns a list of all notifications available in the system for administrative purposes.
        /// **Response Format:**
        /// The response will return a collection of notifications
        /// </remarks>
        /// <returns>Returns a list of all notifications accessible to admin users.</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotification()
        {
            return Ok(await _mediator.Send(new GetAllNotificationQuery()));
        }

        /// <summary>
        /// Retrieves all notifications for the authenticated user.
        /// </summary>
        /// <remarks>
        /// *Only authenticated users can use this API.*
        /// This API returns a list of notifications specific to the currently logged-in user.
        ///
        /// **Request Parameters:**
        /// None
        ///
        /// **Response Format:**
        /// The response will return a collection of notifications
        /// </remarks>
        /// <returns>Returns a list of notifications for the authenticated user.</returns>
        [Authorize(Policy = "User")]
        [HttpGet("user/all")]
        public async Task<IActionResult> GetAllNotificationById()
        {
            return Ok(await _mediator.Send(new GetAllNotificationByUserId()));
        }

        /// <summary>
        /// Retrieves the most recent notification for the authenticated user.
        /// </summary>
        /// <remarks>
        /// *Only authenticated users can use this API.*
        /// This API returns the newest notification specific to the currently logged-in user.
        ///
        /// **Response Format:**
        /// The response will return a single the newest notification.
        ///
        /// </remarks>
        /// <returns>Returns the most recent notification for the authenticated user.</returns>
        [Authorize(Policy = "User")]
        [HttpGet("user/newest")]
        public async Task<IActionResult> GetNewestNotification()
        {
            return Ok(await _mediator.Send(new GetNewestNotificationUserQuery()));
        }

        /// <summary>
        /// Sends a notification to all users.
        /// </summary>
        /// <remarks>
        /// *Only admin users can use this API.*
        /// This API sends a notification to all users in the system based on the provided notification details.
        ///
        /// **Request Parameters:**
        /// - **requestDto** (NotificationRequestDto): The notification details, including:
        ///   - **notificationType** (string): The type of notification. Must be one of: "info", "warning", "success", or "error" or something else.
        ///   - **notificationTitle** (string): The title of the notification.
        ///   - **notificationMessage** (string): The content or message of the notification.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /api/notification/push/all
        /// Content-Type: application/json
        /// {
        ///   "notificationType": "info",
        ///   "notificationTitle": "System Update",
        ///   "notificationMessage": "The system will undergo maintenance this weekend."
        /// }
        /// ```
        /// </remarks>
        /// <returns>Returns a confirmation message if the notification is sent successfully, or an error message if it fails.</returns>
        [Authorize(Policy = "Admin")]
        [HttpPost("push/all")]
        public async Task<IActionResult> PushNotificationToAll([FromBody] NotificationRequestDto requestDto)
        {
            bool check = await _mediator.Send(new PushNotificationForAllUserCommand(requestDto));
            if (check)
            {
                return Ok("Notification sent to user successfully");
            }
            else
            {
                return BadRequest("Failed to send notification to user");
            }
        }

        /// <summary>
        /// Sends a notification to a specific user by their ID.
        /// </summary>
        /// <remarks>
        /// *Only admin users can use this API.*
        /// This API sends a notification to a specific user identified by their user ID, using the provided notification details.
        ///
        /// **Request Parameters:**
        /// - **userId** (int): The unique identifier of the target user.
        /// - **requestDto** (NotificationRequestDto): The notification details, including:
        ///   - **notificationType** (string): The type of notification. Must be one of: "info", "warning", "success", or "error" or something else
        ///   - **notificationTitle** (string): The title of the notification.
        ///   - **notificationMessage** (string): The content or message of the notification.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /api/notification/push/user/1
        /// Content-Type: application/json
        /// {
        ///   "notificationType": "success",
        ///   "notificationTitle": "Task Completed",
        ///   "notificationMessage": "Your task has been successfully completed."
        /// }
        /// ```
        /// </remarks>
        /// <returns>Returns a confirmation message if the notification is sent successfully, or an error message if it fails.</returns>
        [Authorize(Policy = "Admin")]
        [HttpPost("push/user/{userId:int}")]
        public async Task<IActionResult> PushNotificationToUser([FromRoute] int userId, [FromBody] NotificationRequestDto requestDto)
        {
            bool check = await _mediator.Send(new PushNotificationForUserByIdCommand(userId, requestDto));
            if (check)
            {
                return Ok("Notification sent to user successfully");
            }
            else
            {
                return BadRequest("Failed to send notification to user");
            }
        }


        /// <summary>
        /// Updates the status of a specific notification from Unread to Read for the authenticated user.
        /// </summary>
        /// <remarks>
        /// *Only authenticated users can use this API.*
        /// This API updates the status of a notification identified by its ID, marking it as Read for the currently logged-in user.
        ///
        /// **Request Parameters:**
        /// - **notificationId** (int): The unique identifier of the notification to be updated.
        ///
        /// </remarks>
        /// <returns>Returns a confirmation message if the notification status is updated successfully, or an error message if it fails.</returns>
        [Authorize(Policy = "User")]
        [HttpPatch("update/{notificationId:int}")]
        public async Task<IActionResult> UpdateStatusNotification([FromRoute] int notificationId)
        {
            bool check = await _mediator.Send(new UpdateStatusNotificationCommand(notificationId));
            if (check)
            {
                return Ok("Notification status updated successfully");
            }
            else
            {
                return BadRequest("Failed to update notification status");
            }
        }
    }
}

