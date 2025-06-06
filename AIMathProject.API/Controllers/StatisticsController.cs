using AIMathProject.Application.Dto.StatisticsDto;
using AIMathProject.Application.Queries.UserStatistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AIMathProject.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Net.Mime;
using AIMathProject.Application.Queries.Statistics;

namespace AIMathProject.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IMediator mediator, ILogger<StatisticsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Get user statistics for a specific time period
        /// </summary>
        /// <remarks>
        /// This API retrieves comprehensive user statistics based on the specified time period.
        /// 
        /// **Period Types:**
        /// - **day**: Statistics for the current day compared to the previous day
        /// - **week**: Statistics for the current week compared to the previous week
        /// - **month**: Statistics for the current month compared to the previous month
        /// - **year**: Statistics for the current year compared to the previous year
        /// 
        /// **Response includes:**
        /// - User count statistics (current period, previous period, growth rate)
        /// - User engagement metrics (average usage time in minutes, change rate)
        /// - Revenue statistics (current and previous periods, growth rate)
        /// - Error Report statistics (current and previous periods, growth rate)
        /// - Daily breakdown of user activity
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/users/statistics/week
        /// ```
        /// </remarks>
        /// <param name="periodType">The time period type ('day', 'week', 'month', or 'year')</param>
        /// <returns>Returns detailed user statistics for the specified period</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("statistics/{periodType}")]
        public async Task<ActionResult<StatisticsSummaryDto>> GetUserStatistics([FromRoute] string periodType)
        {
            try
            {

                if (string.IsNullOrEmpty(periodType) ||
                    (periodType.ToLower() != "day" &&
                     periodType.ToLower() != "week" &&
                     periodType.ToLower() != "month" &&
                     periodType.ToLower() != "year"))
                {
                    return BadRequest("Invalid period type. Use 'day', 'week', 'month', or 'year'");
                }

                var statistics = await _mediator.Send(new GetUserStatisticsQuery(periodType.ToLower()));
                return Ok(statistics);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving statistics");
            }
        }

        /// <summary>
        /// Get daily revenue by date range with specific time
        /// </summary>
        /// <remarks>
        /// This API retrieves daily revenue data for the specified date range with precise timestamps.
        /// 
        /// **Request Parameters:**
        /// - **startDateTime**: The beginning of the date range (inclusive)
        /// - **endDateTime**: The end of the date range (inclusive)
        /// 
        /// **Response Format:**
        /// The response will return a list of daily revenue entries, each containing:
        /// - **date** (DateTime): The specific date
        /// - **totalRevenue** (decimal): Total revenue generated on that date
        /// - **transactionCount** (int): Number of transactions on that date
        /// - **averageTransaction** (decimal): Average transaction amount on that date
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/revenue/daily/startdate/2025-05-01T08:30:00/enddate/2025-06-01T17:45:00
        /// ```
        /// </remarks>
        /// <returns>Returns a list of daily revenue data for the specified date range</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("revenue/daily/startdate/{startDateTime}/enddate/{endDateTime}")]
        public async Task<ActionResult<List<DailyRevenueDto>>> GetDailyRevenueByDateRange(
            [FromRoute] DateTime startDateTime,
            [FromRoute] DateTime endDateTime)
        {
            if (startDateTime > endDateTime)
            {
                return BadRequest("Start date must be before or equal to end date.");
            }

            var result = await _mediator.Send(new GetDailyRevenueByDateRangeQuery(startDateTime, endDateTime));
            return Ok(result);
        }

        /// <summary>
        /// Get daily error report data by date range with specific time
        /// </summary>
        /// <remarks>
        /// This API retrieves daily error report statistics for the specified date range with precise timestamps.
        /// 
        /// **Request Parameters:**
        /// - **startDateTime**: The beginning of the date range (inclusive)
        /// - **endDateTime**: The end of the date range (inclusive)
        /// 
        /// **Response Format:**
        /// The response will return a list of daily error report entries, each containing:
        /// - **date** (DateTime): The specific date
        /// - **totalErrors** (int): Total number of errors reported on that date
        /// - **resolvedErrors** (int): Number of errors that have been resolved
        /// - **unresolvedErrors** (int): Number of errors that remain unresolved
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/errors/daily/startdate/2025-05-01T08:30:00/enddate/2025-06-01T17:45:00
        /// ```
        /// </remarks>
        /// <returns>Returns a list of daily error report statistics for the specified date range</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("errors/daily/startdate/{startDateTime}/enddate/{endDateTime}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DailyErrorReportDto>>> GetDailyErrorReports(
            [FromRoute] DateTime startDateTime,
            [FromRoute] DateTime endDateTime)
        {
            try
            {
                if (startDateTime > endDateTime)
                {
                    return BadRequest("Start date must be before or equal to end date.");
                }

                var result = await _mediator.Send(new GetDailyErrorReportsQuery
                {
                    StartDate = startDateTime,
                    EndDate = endDateTime
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving daily error reports: {ex.Message}");
            }
        }
    }
}