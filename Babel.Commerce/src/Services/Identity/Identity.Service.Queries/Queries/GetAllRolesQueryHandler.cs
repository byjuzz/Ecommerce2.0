using Identity.Service.Queries.DTOs;
using Identity.Service.Queries.DTOs.Identity.Service.Queries.DTOs;
using Identity.Service.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.Queries.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
    {
        private readonly RoleManager<Identity.Domain.ApplicationRole> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<Identity.Domain.ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleManager.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }
    }
}
