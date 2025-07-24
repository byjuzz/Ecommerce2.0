using Identity.Domain;
using Identity.Service.Queries.Commands;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service.Queries
{
    public class UserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }
    }
}
