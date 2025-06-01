using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Dto.RevenueStatisticsDto;
using AIMathProject.Application.Queries.RevenueStatistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AIMathProject.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RevenueStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RevenueStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get revenue statistics for a specific time period
        /// </summary>
        /// <remarks>
        /// *Only admin users can access this API*
        /// 
        /// This API retrieves comprehensive revenue statistics based on the specified time period.
        /// 
        /// **Period Types:**
        /// - **day**: Statistics for the current day compared to the previous day
        /// - **week**: Statistics for the current week compared to the previous week
        /// - **month**: Statistics for the current month compared to the previous month
        /// - **year**: Statistics for the current year compared to the previous year
        /// 
        /// **Response includes:**
        /// - **currentRevenue**: Total revenue for the current period
        /// - **previousRevenue**: Total revenue for the previous equivalent period
        /// - **growthRate**: Percentage change between current and previous period
        /// - **period**: Details about the time period including start date, end date, and period type
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /api/revenue/statistics/period/month
        /// ```
        /// </remarks>
        /// <param name="periodType">The time period type (day, week, month, year)</param>
        /// <returns>Revenue statistics for the specified period</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("revenue/statistics/period/{periodType}")]
        public async Task<IActionResult> GetRevenueStatistics([FromRoute] string periodType)
        {
            var query = new GetRevenueStatisticsQuery(periodType);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Thêm endpoint này vào RevenueStatisticsController
        /// <summary>
        /// Get daily revenue by date range with specific time
        /// </summary>
        /// 
        /// /// **Example Request:**
        /// ```http
        /// GET /api/dailyrevenue/startdate/2025-05-01T08:30:00/enddate/2025-06-01T17:45:00
        /// ```
        /// <param name="startDateTime">Start date and time (format: yyyy-MM-ddTHH:mm:ss)</param>
        /// <param name="endDateTime">End date and time (format: yyyy-MM-ddTHH:mm:ss)</param>
        /// <returns>List of daily revenue data</returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("dailyrevenue/startdate/{startDateTime}/enddate/{endDateTime}")]
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
    }
}