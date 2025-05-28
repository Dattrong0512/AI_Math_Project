using AIMathProject.Application.Queries.TokenPackage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIMathProject.API.Controllers
{
    [Route("api/tokenpackage")]
    [ApiController]
    public class TokenPackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenPackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Only logged user can use this api.
        /// This API return all token package.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTokenPackage()
        {
            return Ok(await _mediator.Send(new GetAllTokenPackageQuery()));
        }

   
    }
}
