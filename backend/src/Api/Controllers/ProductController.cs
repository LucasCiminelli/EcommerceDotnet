using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductList()
        {
            var query = new GetProductListQuery();

            var productList = await _mediator.Send(query); //el mediatr le envia el query al query handler y el query handler devuelve la data. en este caso la lista de productos


            return Ok(productList);
        }

    }
}