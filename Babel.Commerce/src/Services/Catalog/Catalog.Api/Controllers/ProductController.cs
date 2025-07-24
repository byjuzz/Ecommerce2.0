using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries.DTO;
using Catalog.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Catalog.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ISender _mediator;
        public ProductController(ILogger<ProductController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> products = null;
            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _mediator.Send(new GetProductsQuery(Page: page, Take: take, Products: products));
        }

        [HttpGet("{id}", Name = "GetproductById")]
        public async Task<ProductDto> Get(int id)
        {
            return await _mediator.Send(new GetProductQuery(Id: id));
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            var result = await _mediator.Send(command);
            //return Ok();
            //return StatusCode(201);
            return CreatedAtRoute("GetproductById", new { id = result }, result);
        }
    }
}
