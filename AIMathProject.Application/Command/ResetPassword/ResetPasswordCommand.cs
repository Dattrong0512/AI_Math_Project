using AIMathProject.Application.Dto;
using AIMathProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordModel resetPassword) : IRequest<string>;
    public class ResetPasswordHandler(UserManager<User> _userManager) : IRequestHandler<ResetPasswordCommand, string>
    {
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.resetPassword.Email);

            if (user == null)
            {
                throw new Exception($"Email {request.resetPassword.Email} not exists");
            }
            if (string.IsNullOrEmpty(request.resetPassword.Token))
            {
                throw new Exception("Token is invalid");
            }
            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.resetPassword.Token));

            var identityResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.resetPassword.Password);

            if (identityResult.Succeeded)
            {
                return ("Reset password successful");
            }
            return ("Reset password fail");
        }
    }
}
