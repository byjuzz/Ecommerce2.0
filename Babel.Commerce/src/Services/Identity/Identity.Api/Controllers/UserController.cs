using Identity.Service.EventHandlers.Commands;
using Identity.Service.Queries.Commands;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using System.Security.Claims;

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

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirst("id")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _mediator.Send(new GetUserQuery(userId));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> CreateUserByAdmin([FromBody] UserCreateByAdminCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Error creando usuario o asignando rol");

            return Ok("Usuario creado correctamente con rol asignado");
        }
    }
}
