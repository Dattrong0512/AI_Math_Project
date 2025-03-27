using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Command.Register
{
    public class RegisterCommand : IRequest<Unit>
    {
        public RegisterRequest RegisterRequest { get; set; }
        public string Role;
        public RegisterCommand(RegisterRequest registerRequest,string role)
        {
            RegisterRequest = registerRequest;
            Role = role;
        }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RegisterCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByEmailAsync(request.RegisterRequest.Email) != null;
            if (userExists)
            {
                throw new UserAlreadyExistsException(email: request.RegisterRequest.Email);
            }

            var user = User.Create(request.RegisterRequest.UserName, request.RegisterRequest.Email, request.RegisterRequest.Gender,
                request.RegisterRequest.Dob, request.RegisterRequest.Avatar);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.RegisterRequest.Password);

            var result = await _userManager.CreateAsync(user);

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole<int>(request.Role));
                if (!roleResult.Succeeded)
                {
                    throw new Exception("Can't create User Role");
                }
            }
                var addToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addToRoleResult.Succeeded)
            {
                throw new Exception("Can't assign User Role to user: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
            }

            return Unit.Value;
        }
    }
}