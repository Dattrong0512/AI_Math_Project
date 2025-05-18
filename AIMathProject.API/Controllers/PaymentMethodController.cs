using AIMathProject.Application.Queries.PaymentMethod;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("api/paymentmethod")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PaymentMethodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Only logged user can use this api (include admin and user)
        /// This API return all Payment method.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethod()
        {
            return Ok(await _mediator.Send(new GetAllPaymentMethodQuery()));
        }
    }
}
