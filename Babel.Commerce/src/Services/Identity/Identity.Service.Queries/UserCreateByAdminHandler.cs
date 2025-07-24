using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Queries
{
    public class UserCreateByAdminHandler : IRequestHandler<UserCreateByAdminCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserCreateByAdminHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(UserCreateByAdminCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return false;

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                var roleCreated = await _roleManager.CreateAsync(new ApplicationRole { Name = request.Role });
                if (!roleCreated.Succeeded) return false;
            }

            await _userManager.AddToRoleAsync(user, request.Role);
            return true;
        }
    }
}
