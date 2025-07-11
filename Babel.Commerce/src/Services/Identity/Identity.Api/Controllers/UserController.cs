using Identity.Service.Queries.Commands;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(
            ILogger<UserController> logger,
            IMediator mediator
            )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<string> users = ids?.Split(',');
            return await _mediator.Send(new GetUsersQuery(page, take, users));
        }

        [HttpGet("{id}")]
        public async Task<UserDto> Get(string id)
        {
            return await _mediator.Send(new GetUserQuery(id));

        }
    }
}
