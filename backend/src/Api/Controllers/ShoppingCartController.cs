using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart;
using Ecommerce.Application.Features.ShoppingCarts.Queries.GetShoppingCartById;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingCartController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVm), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCartVm>> GetShoppingCart(Guid id)
        {
            var shoppingCartId = id == Guid.Empty ? Guid.NewGuid() : id; //cuando no tenes carritos creados, el primer carrito tenes que mandarlo con valor de Guid.Empty = 00000000-0000-0000-0000-000000000000
            var query = new GetShoppingCartByIdQuery(shoppingCartId);


            return await _mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPut("{id}", Name = "UpdateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartVm), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCartVm>> UpdateShoppingCart(Guid id, UpdateShoppingCartCommand request)
        {
            request.ShoppingCartId = id;

            return await _mediator.Send(request);
        }


    }
}