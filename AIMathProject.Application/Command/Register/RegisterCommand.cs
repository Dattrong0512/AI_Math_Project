using AIMathProject.Application.Abstracts;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Exceptions;
using AIMathProject.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Policy;


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

        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IEmailHelper emailHelper, IEmailTemplateReader emailTemplateReader, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByEmailAsync(request.RegisterRequest.Email) != null;

            if (userExists)
            {
                throw new UserAlreadyExistsException(email: request.RegisterRequest.Email);
            }

            var user = User.Create(request.RegisterRequest.UserName, request.RegisterRequest.Email,
                request.RegisterRequest.Dob, request.RegisterRequest.PhoneNumber);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.RegisterRequest.Password);

            var result = await _userManager.CreateAsync(user);

            await _userRepository.SendEmailConfirm(user, cancellationToken);

            if (request.Role == "Admin")
            {
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
            }
          
            return Unit.Value;
        }
    }
}