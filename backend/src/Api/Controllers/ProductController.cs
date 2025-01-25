using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous] //consumido de manera pública. Sin necesidad de tener credenciales.
        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
        {
            var query = new GetProductListQuery();

            var productList = await _mediator.Send(query); //el mediatr le envia el query al query handler y el query handler devuelve la data. en este caso la lista de productos


            return Ok(productList);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationProduct")]
        [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductsQuery paginationProductQuery)
        {

            paginationProductQuery.Status = ProductStatus.Activo;

            var paginationProduct = await _mediator.Send(paginationProductQuery);

            return Ok(paginationProduct);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductById(int id)
        {

            var query = new GetProductByIdQuery(id);

            var product = await _mediator.Send(query);


            return Ok(product);

        }

    }
}