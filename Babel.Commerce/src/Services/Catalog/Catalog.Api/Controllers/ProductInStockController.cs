using Catalog.Service.Queries.DTO;
using Catalog.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using Catalog.Service.EventHandlers.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("stocks")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController> _logger;
        private readonly IMediator _mediator;
        public ProductInStockController(IMediator mediator, ILogger<ProductInStockController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
               
        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }
    }
}
