using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AIMathProject.Application.Command.Register
{
    public record ConfirmEmailCommand(string IdUser, string token) : IRequest<string>;
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, string>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;
        public ConfirmEmailHandler(RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.IdUser);
            if (user == null)
            {
                throw new Exception("User not exists");
            }
            var identity = await _userManager.ConfirmEmailAsync(user, request.token);
            if(identity.Succeeded)
            {
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
                return $"Confirm email {user.Email} successful";
            }
            return "Confirm email fail";
        }
    }
}

