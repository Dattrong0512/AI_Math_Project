using System;
using System.Threading;
using System.Threading.Tasks;
using AIMathProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AIMathProject.Application.Command.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return Unit.Value;
        }
    }
}
