using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Command.Login;
using AIMathProject.Application.Command.RefreshToken;
using AIMathProject.Application.Command.Register;
using AIMathProject.Application.Command.ResetPassword;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Queries.ResetPassword;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Requests;
using AIMathProject.Infrastructure.CommonServices;
using AIMathProject.Infrastructure.Options;
using AIMathProject.Infrastructure.Processors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.API.Controllers
{
    [Route("")]
    [ApiController]
    public class LoginWithUSPW : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LoginWithUSPW> _logger;
        public LoginWithUSPW(IMediator mediator,  ILogger<LoginWithUSPW> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <remarks>
        /// This API allows the creation of a new user account with the provided registration details.
        ///
        /// **Request:**
        /// The request body must contain the user information:
        /// - **UserName**: The username for the account.
        /// - **Email**: The email address associated with the account.
        /// - **Dob**: The date of birth of the user.
        /// - **PhoneNumber**: The phone number of the user.
        /// - **Password**: The password for the account(Consists of at least 8 characters, with uppercase and lowercase letters).
        ///
        /// **Example Request:**
        /// ```http
        /// POST /account/register/user
        /// Content-Type: application/json
        /// {
        ///     "UserName": "john_doe",
        ///     "Email": "john.doe@example.com",
        ///     "Dob": "1990-01-01T00:00:00",
        ///     "PhoneNumber": "0909909090",
        ///     "Password": "securepasswordAa"
        /// }
        /// ```
        /// </remarks>
        /// <param name="registerRequest">The registration details for the user.</param>
        /// <returns>Returns a success response if the registration was successful.</returns>
        [HttpPost("account/register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Register request cannot be null.");
            }
            await _mediator.Send(new RegisterCommand(registerRequest,"User"));
       

            return Ok("Vui lòng xác thực email");
        }
        /// <summary>
        /// Registers a new admin account.
        /// </summary>
        /// <remarks>
        /// This API allows the creation of a new admin account with the provided registration details.
        ///
        /// **Request:**
        /// The request body must contain the admin information:
        /// - **UserName**: The username for the account.
        /// - **Email**: The email address associated with the account.
        /// - **Gender**: The gender of the admin.
        /// - **Dob**: The date of birth of the admin.
        /// - **Avatar**: The avatar image URL for the admin.
        /// - **Password**: The password for the account(Consists of at least 8 characters, with uppercase and lowercase letters).
        /// - **Status**: The status of the account (active/inactive).
        ///
        /// **Example Request:**
        /// ```http
        /// POST /account/register/admin
        /// Content-Type: application/json
        /// {
        ///     "UserName": "admin_john",
        ///     "Email": "admin.john@example.com",
        ///     "Dob": "1985-01-01T00:00:00",
        ///     "PhoneNumber": "0909909090",
        ///     "Password": "adminpassword"
        /// }
        /// ```
        /// </remarks>
        /// <param name="registerRequest">The registration details for the admin.</param>
        /// <returns>Returns a success response if the registration was successful.</returns>
        [HttpPost("account/register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Register request cannot be null.");
            }
            await _mediator.Send(new RegisterCommand(registerRequest,"Admin"));

            return Ok();
        }

        /// <summary>
        /// Logs in a user and returns JWT and refresh tokens.
        /// </summary>
        /// <remarks>
        /// This API handles user login by verifying credentials and issuing tokens.
        ///
        /// **Request:**
        /// The request body must contain the login information:
        /// - **Email**: The user's email address.
        /// - **Password**: The user's password.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /account/login
        /// Content-Type: application/json
        /// {
        ///     "Email": "michael.brown@example.com",
        ///     "Password": "Michael@101"
        /// }
        /// ```
        /// </remarks>
        /// <param name="loginRequest">The login details for the user.</param>
        /// <returns>Returns JWT and refresh tokens if the login is successful and it saved in cookies</returns>
        [HttpPost("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            _logger.LogInformation("Received login request for email: {Email}", loginRequest?.Email);

            if (loginRequest == null)
            {
                _logger.LogWarning("Login request is null.");
                return BadRequest("Login request cannot be null.");
            }

            try
            {
                var (jwtToken, refreshToken) = await _mediator.Send(new LoginCommand(loginRequest));
                if(refreshToken == null)
                {
                    return BadRequest("Vui lòng xác thực email trước khi đăng nhập");
                }
                _logger.LogInformation("Login successful for email: {Email}. Returning tokens: JWT={JwtToken}, RefreshToken={RefreshToken}", loginRequest.Email, jwtToken, refreshToken);
                return Ok(new { JwtToken = jwtToken, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for email: {Email}. Error: {ErrorMessage}", loginRequest.Email, ex.Message);
                throw; // Ném lại ngoại lệ để middleware xử lý (nếu cần)
            }
        }

        /// <summary>
        /// Refreshes a JWT token using a refresh token and returns a new JWT token and refresh token.
        /// </summary>
        /// <remarks>
        /// This API allows a client to refresh an expired JWT token by providing a valid refresh token.
        /// A new JWT token and a new refresh token will be issued if the provided refresh token is valid.
        ///
        /// **Request:**
        /// The request body must contain the refresh token:
        /// - **refreshTokenRequest**: The refresh token previously issued to the client.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /api/account/refresh-token
        /// Content-Type: application/json
        /// {
        ///     "refreshTokenRequest": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
        /// }
        /// </remarks>
        /// <param name="refreshTokenRequest">The refresh token provided by the client to obtain a new JWT token.</param>
        /// <returns>
        /// Returns a new JWT token and a new refresh token if the refresh token is valid.
        /// Returns a 400 Bad Request if the refresh token is null or empty.
        /// </returns>
        [HttpPost("api/account/refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshTokenRequest)
        {
            if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest))
            {
                return BadRequest("Refresh token cannot be null or empty.");
            }

            var (jwtToken, newRefreshToken) = await _mediator.Send(new RefreshTokenCommand(refreshTokenRequest));
            return Ok(new { JwtToken = jwtToken, RefreshToken = newRefreshToken });
        }

        /// <summary>
        /// Initiates the forgot password process by sending a reset password link to the user's email.
        /// </summary>
        /// <param name="email">The email address of the user requesting a password reset.</param>
        /// <param name="host">The host URL used to construct the password reset link (e.g., "http://localhost:5173/forgot-password").</param>
        /// <returns>
        /// Returns an <see cref="OkObjectResult"/> if the reset password request is successfully initiated,
        /// or a <see cref="BadRequestObjectResult"/> if an error occurs during the process.
        /// </returns>
        /// <remarks>
        /// - This endpoint does not require authentication.
        /// - The <paramref name="host"/> parameter should be the base URL of the frontend application
        ///   where the user will be redirected to reset their password.
        /// - If the email is not found or an error occurs, a bad request response is returned with an error message.
        /// </remarks>
        [HttpGet("account/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email,
           string host)
        {

            try
            {
                return Ok(await _mediator.Send(new ResetPasswordQuery(email, host)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Initiates the forgot password process by sending a reset password link to the user's email.
        /// </summary>
        /// <param name="email">The email address of the user requesting a password reset.</param>
        /// <param name="host">The host URL used to construct the password reset link (e.g., "http://localhost:5173/forgot-password").</param>
        /// <returns>
        /// Returns an <see cref="OkObjectResult"/> if the reset password request is successfully initiated,
        /// or a <see cref="BadRequestObjectResult"/> if an error occurs during the process.
        /// </returns>
        /// <remarks>
        /// - This endpoint does not require authentication.
        /// - The <paramref name="host"/> parameter should be the base URL of the frontend application
        ///   where the user will be redirected to reset their password.
        /// - If the email is not found or an error occurs, a bad request response is returned with an error message.
        /// - Frontend code reference: https://drive.google.com/drive/folders/18M__nFfmDoVOyqHElTmNdyTL7PiEfbXX?usp=sharing
        /// </remarks>
        [HttpPost("account/reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                return Ok(await _mediator.Send(new ResetPasswordCommand(resetPassword)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// This API uses a callback when the user clicks on the email confirmation link. This API is not used by the front-end.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            return Ok(await _mediator.Send(new ConfirmEmailCommand(userId, token)));
        }



        /// <summary>
        /// Api test if user logged(Both admin and user)
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("api/account/test-authentication")]
        public async Task<IActionResult> TestAuthentication()
        {
            return Ok("You are authenticated");
        }

        /// <summary>
        /// This API is used to test users who have logged in and have the Role of Admin.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("api/account/test-authentication-admin")]
        public async Task<IActionResult> TestAdminOnly()
        {
            return Ok("You are Admin");
        }
        /// <summary>
        /// This API is used to test users who have logged in and have the Role of user.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "User")]
        [HttpGet("api/account/test-authentication-user")] // Đổi thành chữ thường
        public async Task<IActionResult> TestUserRole()
        {
            return Ok("You are User");
        }
    }
}