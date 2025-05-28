using AIMathProject.Application.Command.Wallet;
using AIMathProject.Application.Dto.Payment.Wallet;
using AIMathProject.Application.Dto.UpdateWalletRequests;
using AIMathProject.Application.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get a wallet by user ID.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Update the amount of tokens used in a wallet.
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPatch("token")]
        public async Task<ActionResult<WalletDto>> UpdateTokenUsed([FromBody] UpdateWalletRequest wallet)
        {
            return await _mediator.Send(new UpdateTokenUsedCommand(wallet.WalletId, wallet.Amount));

        }
        /// <summary>
        /// Update the amount of coins used in a wallet.
        /// </summary>
        /// <param name="walletId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPatch("coin")]
        public async Task<ActionResult<WalletDto>> UpdateCoinUsed([FromBody] UpdateWalletRequest wallet)
        {
            return await _mediator.Send(new UpdateCoinUsedCommand(wallet.WalletId, wallet.Amount));
        }
    }
}
