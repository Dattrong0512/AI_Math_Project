using AI_Math_Project.DTO.LoginDto;
using AI_Math_Project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace AI_Math_Project.Controller
{
    [Route("api")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IConfiguration _confi;
        private readonly IRefreshTokenRepository _refresh;

        public JWTController(IAccountRepository repo, IConfiguration configuration, IRefreshTokenRepository refresh)
        {
            _repo = repo;
            _confi = configuration;
            _refresh = refresh;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<LoginResponseDto>> LoginJWT([FromBody]LoginRequestDto request)
        {
            var account = await _repo.Login(request.Email, request.Password);
            if(account == null)
            {
                return Unauthorized("Invalid Email or password.");
            }
            //Generate JWT Token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("Role", account.Role.ToString()),
                new Claim("AccountID", account.AccountId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confi["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var preparedToken = new JwtSecurityToken(
                issuer: _confi["JWT:Issuer"],
                audience: _confi["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(10),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(preparedToken);

            string checkExists = await _refresh.CheckRefreshToken(account.AccountId);
            string RefreshToken;
            if (checkExists == "RefreshToken is not exists")
            {
                //Generate RefreshToken
                RefreshToken = await _refresh.SaveRefreshToken(account.AccountId); // Lưu Refresh Token vào cơ sở dữ liệu
            }
            else
            {
                RefreshToken = checkExists;
            }

                var role = account.Role.ToString();
            var accountID = account.AccountId;
            return Ok(new LoginResponseDto
            { 
                Token=token,
                Role= role,
                RefreshToken = RefreshToken,
                AccountId = accountID
            });


        }
        [HttpPost("/refresh-token")]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {//Chua xu ly tinh huong RefreshToken co het han hay chua, neu het thi login lai.

            var account = await _repo.GetInfo(request.AccountId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("Role", account.Role.ToString()),
                new Claim("AccountID", account.AccountId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confi["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var preparedToken = new JwtSecurityToken(
                issuer: _confi["JWT:Issuer"],
                audience: _confi["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(7).AddSeconds(10),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(preparedToken);                   
            var role = account.Role.ToString();
            var accountID = account.AccountId;
            return Ok(new LoginResponseDto
            {
                Token = token,
                Role = role,
                RefreshToken = request.Token,
                AccountId = accountID
            });
        }





        // 1. Test Authentication: Xac thuc
        [Authorize]
        [HttpGet("/test-authentication")]
        public async Task<IActionResult> TestAuthentication()
        {
            return Ok("You are authenticated");
        }

        // 2. Test Authorization: AdminOnly
        [Authorize(Policy = "Admin")]
        [HttpGet("/test-authentication-admin")]
        public async Task<IActionResult> TestAdminOnly()
        {
            return Ok("You are Admin");
        }

        // 3. Test Authorization: AdminOrStaff
        [Authorize(Policy = "User")]
        [HttpGet("/test-authentication-User")]
        public async Task<IActionResult> TestAdminOrStaff()
        {
            return Ok("You are User");
        }
    }
}
