using AIMathProject.Application.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("user/{UserId:int}")]
        public async Task<IActionResult> GetWalletByUserId(int UserId)
        {
            var wallet = await _mediator.Send(new GetWalletByUserIdQuery(UserId));
            if (wallet == null)
            {
                return NotFound();
            }
            return Ok(wallet);
        }
    }
}
