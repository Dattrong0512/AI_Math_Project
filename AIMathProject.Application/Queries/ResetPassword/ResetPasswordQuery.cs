using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto;
using AIMathProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace AIMathProject.Application.Queries.ResetPassword
{
    public record ResetPasswordQuery(string email, string host): IRequest<string>;

    public class ResetPasswordHandler(UserManager<User> _userManager,
        IEmailHelper _emailSender) : IRequestHandler<ResetPasswordQuery, string>
    {
        public async Task<string> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                throw new Exception($"Email {request.email} not exists");
            }

            var check = await _userManager.FindByLoginAsync("Google", request.email);
            if(check != null)
            {
                return "Người dùng đăng nhập bằng phương thức Google không thể sử dụng tính năng này";
            }

            var tokenConfirm = await _userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenConfirm));
            string resetPasswordUrl = $"{request.host}/reset-password?email={request.email}&token={encodedToken}";

            string body = $"Please reset your password by clicking here: <a href=\"{resetPasswordUrl}\">link</a>";

            await _emailSender.SendEmailAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Reset Password",
                Content = body
            }, cancellationToken);
            return "Please check your email";
        }
    }
}
