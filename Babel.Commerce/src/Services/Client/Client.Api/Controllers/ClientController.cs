using Client.Service.EventHandlers.Commands;
using Client.Service.Queries.DTO;
using Client.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Client.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("clients")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ISender _mediator;
        public ClientController(ILogger<ClientController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> Clients = null;
            if (!string.IsNullOrEmpty(ids))
            {
                Clients = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _mediator.Send(new GetClientsQuery(Page: page, Take: take, Clients: Clients));
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<ClientDto> Get(int id)
        {
            return await _mediator.Send(new GetClientQuery(Id: id));
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ClientCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetClientById", new { id = result }, result);
        }
    }
}
