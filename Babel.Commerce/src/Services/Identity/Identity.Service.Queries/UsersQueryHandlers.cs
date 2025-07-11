using Azure;
using Identity.Persistence.Database;
using Identity.Service.Queries.Commands;
using Identity.Service.Queries.DTOs;
using Service.Common.Paging;
using MediatR;
using Service.Common.Collection;
using Service.Common.Mapping;

namespace Identity.Service.Queries
{
    public class UsersQueryHandlers : IRequestHandler<GetUsersQuery, DataCollection<UserDto>>
    {
        private readonly ApplicationDbContext _context;

        public UsersQueryHandlers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var collection = await _context.Users
                 .Where(x => request.Users == null || request.Users.Contains(x.Id))
                 .OrderBy(x => x.FirstName)
                 .GetPagedAsync(request.Page, request.Take);

            return collection.MapTo<DataCollection<UserDto>>();
        }
    }
}
