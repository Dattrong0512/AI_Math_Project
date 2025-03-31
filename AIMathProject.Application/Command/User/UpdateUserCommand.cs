using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Requests;
using AIMathProject.Domain.Entities;
using System.Security.Permissions;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using AIMathProject.Application.Abstracts;

namespace AIMathProject.Application.Command.Users
{
    public  class UpdateUserCommand : IRequest<Unit>
    {
        public UpdateRequest UpdateRequest { get; set; }
        public UpdateUserCommand(UpdateRequest updateRequest)
        {
            UpdateRequest = updateRequest;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }


        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var usr = await _userRepository.GetInfoUserLogin();

            var user = await _userManager.FindByIdAsync(usr.UserId.ToString());
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            var properties = typeof(UpdateRequest).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == nameof(usr)) continue;

                var value = property.GetValue(request.UpdateRequest);
                if (value != null)
                {
                    var propertyInfo = typeof(User).GetProperty(property.Name);
                    propertyInfo.SetValue(user, value);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return Unit.Value;
        }
    }

}
