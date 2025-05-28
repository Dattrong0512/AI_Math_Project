using AIMathProject.Application.Queries.Plans;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("plan")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Only logged user can use this api (include user or admin)
        /// This api return all plan to user buy it
        /// </summary>
        /// <returns></returns>
        //[Authorize(Policy = "UserOrAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllPlan()
        {
            return Ok(await _mediator.Send(new GetAllPlansQuery()));
        }
    }
}
