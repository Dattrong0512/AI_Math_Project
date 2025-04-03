using AIMathProject.Application.Command.Login;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AIMathProject.API.Controllers
{
    [Route("")]
    [ApiController]
    public class LoginWithGoogleController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMediator _mediator;

        public LoginWithGoogleController(
            SignInManager<User> signInManager,
            LinkGenerator linkGenerator,
            IMediator mediator)
        {
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
            _mediator = mediator;
        }
        /// <summary>
        /// Initiates the login process via Google.
        /// </summary>
        /// <remarks>
        /// This endpoint starts the Google authentication process. It redirects to the Google login page.
        /// The request should include the `returnUrl` query parameter that will be used after successful authentication.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /account/login/google?returnUrl=https://example.com/dashboard
        /// ```
        /// </remarks>
        /// <param name="returnUrl">The URL to redirect to after successful login.</param>
        /// <returns>Returns a challenge to Google authentication.</returns>
        [HttpGet("account/login/google")]
        public IResult LoginWithGoogle([FromQuery] string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "LoginWithGoogle", new { returnUrl }, Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Results.Challenge(properties, ["Google"]);
        }

        /// <summary>
        /// Handles the callback from Google after authentication, do not used by front-end
        /// </summary>
        /// <remarks>
        /// This endpoint is triggered after the user has authenticated with Google.
        /// If authentication is successful, the user's principal is passed to a command for login processing.
        /// If authentication fails, the request is rejected.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /account/login/google/callback?returnUrl=https://example.com/dashboard
        /// ```
        /// </remarks>
        /// <param name="returnUrl">The URL to redirect to after successful login.</param>
        /// <returns>Redirects the user to the return URL if login is successful, or unauthorized if failed.</returns>
        [HttpGet("account/login/google/callback", Name = "GoogleLoginCallback")] 
        public async Task<IResult> GoogleLoginCallback([FromQuery] string returnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Results.Unauthorized();
            }
            try
            {
                await _mediator.Send(new LoginWithGoogleCommand(result.Principal));
                return Results.Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                if(ex is ExternalLoginProviderException)
                {
                    return Results.Conflict(new
                    {
                        Message = ex.Message,
                        Provider = "Google"
                    });
                }
                else
                {
                    return Results.StatusCode(500);
                }
            }
        }
    }
}