using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Login
{
    public class LoginWithGoogleCommand : IRequest<Unit>
    {
        public ClaimsPrincipal claimsPrincipal { get; set; }

        public LoginWithGoogleCommand(ClaimsPrincipal claimsPrincipal)
        {
            this.claimsPrincipal = claimsPrincipal;
        }

    }
    public class LoginCommandGoogleHandler : IRequestHandler<LoginWithGoogleCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public LoginCommandGoogleHandler(UserManager<User> userManager, IAuthTokenProcessor authTokenProcessor, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _authTokenProcessor = authTokenProcessor;
            _roleManager = roleManager;
        }


        public async Task<Unit> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            if (request.claimsPrincipal == null)
            {
                throw new ExternalLoginProviderException("Google",
                    "ClaimsPrincipal is null");
            }
            var email = request.claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                throw new ExternalLoginProviderException("Google", "Email is null");
            }
            string genderCheck = request.claimsPrincipal.FindFirstValue(ClaimTypes.Gender);
            bool gender = false;
            if (genderCheck == "Male")
            {
                gender = true;
            }
            var userName = request.claimsPrincipal.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByEmailAsync(email);
            DateTime parsedDate;
            if (string.IsNullOrEmpty(request.claimsPrincipal.FindFirstValue(ClaimTypes.DateOfBirth)))
            {
                parsedDate = DateTime.UtcNow;
            }
            else
            {
                parsedDate = DateTime.Parse(request.claimsPrincipal.FindFirstValue(ClaimTypes.DateOfBirth));
            }
            if (user == null)
            {
                // Tạo người dùng mới nếu chưa tồn tại
                var newUser = new User
                {
                    UserName = userName,
                    Email = email,
                    Gender = gender,
                    Dob = parsedDate,
                    Avatar = "null",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    throw new ExternalLoginProviderException("Google",
                        $"Unable to create user: {string.Join(", ", result.Errors.Select(x => x.Description))}");
                }
                user = newUser;

                var info = new UserLoginInfo("Google",
                    request.claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty, "Google");
                var loginResult = await _userManager.AddLoginAsync(user, info);
                if (!loginResult.Succeeded)
                {
                    throw new ExternalLoginProviderException("Google",
                        $"Unable to add login: {string.Join(", ", loginResult.Errors.Select(x => x.Description))}");
                }
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole<int>("User"));
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Can't create User Role");
                    }
                }
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception("Can't assign User Role to user: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Nếu người dùng đã tồn tại, kiểm tra thông tin đăng nhập từ Google
                var logins = await _userManager.GetLoginsAsync(user);
                var googleLogin = logins.FirstOrDefault(l => l.LoginProvider == "Google");

                if (googleLogin == null)
                {     
                    throw new ExternalLoginProviderException("Google",
                        $"Unable to register: user has email {email} already exists, the user has registered an account using another method.");
                 
                }
            }

            // Tạo mới refresh token và access token
            var (jwtToken, expirationDateInUtc) = await _authTokenProcessor.GenerateJwtToken(user);
            var refreshToken = _authTokenProcessor.GenerateRefreshToken();
            var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(90);

            // Cập nhật refresh token vào người dùng
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiredAtUtc = refreshTokenExpirationDateInUtc;
            await _userManager.UpdateAsync(user);

           
            // Ghi token vào cookie
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken, refreshTokenExpirationDateInUtc);

            return Unit.Value;

        }
    }

}
