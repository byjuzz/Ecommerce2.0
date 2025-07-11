using Identity.Persistence.Database;
using Identity.Service.Queries.Commands;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Common.Mapping;

namespace Identity.Service.Queries
{
    public class UserQueryHandler :IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly ApplicationDbContext _context;

        public UserQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Users.SingleAsync(x => x.Id == request.Id)).MapTo<UserDto>();
        }

    }
}
