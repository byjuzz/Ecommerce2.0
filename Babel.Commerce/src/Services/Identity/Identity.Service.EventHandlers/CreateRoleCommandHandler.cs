using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service.EventHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IdentityResult>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            // Validar existencia
            if (await _roleManager.RoleExistsAsync(request.RoleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"El rol '{request.RoleName}' ya existe."
                });
            }

            var role = new ApplicationRole
            {
                Name = request.RoleName
            };

            return await _roleManager.CreateAsync(role);
        }
    }
}
