using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        
        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;

        }       

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> Orders = null;

            if (!string.IsNullOrEmpty(ids))
            {
                Orders = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _mediator.Send(new GetOrdersQuery(Page: page, Take: take, Orders: Orders));
        }
       
        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            return await _mediator.Send(new GetOrderQuery(Id: id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
