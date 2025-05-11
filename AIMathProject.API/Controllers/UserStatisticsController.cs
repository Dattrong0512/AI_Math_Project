//using AIMathProject.Application.Dto.UserStatisticsDto;
//using AIMathProject.Application.Queries.UserStatistics;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AIMathProject.Domain.Interfaces;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading.Tasks;
//using System.Net.Mime;

//namespace AIMathProject.API.Controllers
//{
//    [Route("api")]
//    [ApiController]
//    [Produces(MediaTypeNames.Application.Json)]
//    [Consumes(MediaTypeNames.Application.Json)]
//    public class UserStatisticsController : ControllerBase
//    {
//        private readonly IMediator _mediator;
//        private readonly ILogger<UserStatisticsController> _logger;

//        public UserStatisticsController(IMediator mediator, ILogger<UserStatisticsController> logger)
//        {
//            _mediator = mediator;
//            _logger = logger;
//        }
//        /// <summary>
//        /// Get user statistics for a specific time period
//        /// </summary>
//        /// <remarks>
//        /// This API retrieves comprehensive user statistics based on the specified time period.
//        /// 
//        /// **Period Types:**
//        /// - **day**: Statistics for the current day compared to the previous day
//        /// - **week**: Statistics for the current week compared to the previous week
//        /// - **month**: Statistics for the current month compared to the previous month
//        /// - **year**: Statistics for the current year compared to the previous year
//        /// 
//        /// **Response includes:**
//        /// - User count statistics (current period, previous period, growth rate)
//        /// - User engagement metrics (average usage time in minutes, change rate)
//        /// - Daily breakdown of user activity
//        /// 
//        /// **Example Request:**
//        /// ```http
//        /// GET /api/users/statistics/week
//        /// ```
//        /// </remarks>
//        /// <param name="periodType">The time period type ('day', 'week', 'month', or 'year')</param>
//        /// <returns>Returns detailed user statistics for the specified period</returns>
//        [Authorize(Policy = "Admin")]
//        [HttpGet("users/statistics/{periodType}")]
//        public async Task<ActionResult<UserStatisticsSummaryDto>> GetUserStatistics([FromRoute] string periodType)
//        {
//            try
//            {
//                _logger.LogInformation($"Getting user statistics for period: {periodType}");

//                if (string.IsNullOrEmpty(periodType) ||
//                    (periodType.ToLower() != "day" &&
//                     periodType.ToLower() != "week" &&
//                     periodType.ToLower() != "month" &&
//                     periodType.ToLower() != "year"))
//                {
//                    return BadRequest("Invalid period type. Use 'day', 'week', 'month', or 'year'");
//                }

//                var statistics = await _mediator.Send(new GetUserStatisticsQuery(periodType.ToLower()));
//                return Ok(statistics);
//            }
//            catch (ArgumentException ex)
//            {
//                _logger.LogError(ex, $"Invalid argument: {ex.Message}");
//                return BadRequest(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting user statistics");
//                return StatusCode(500, "An error occurred while retrieving user statistics");
//            }
//        }
//    }
//}