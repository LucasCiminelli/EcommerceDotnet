using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Orders.Commands.CreateOrder;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Application.Features.Orders.Queries.GetOrderById;
using Ecommerce.Application.Features.Orders.Queries.PaginationOrders;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public OrderController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("address", Name = "CreateAddress")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<AddressVm>> CreateAddress([FromBody] CreateAddressCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<OrderVm>> CreateOrder([FromBody] CreateOrderCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<OrderVm>> UpdateOrder([FromBody] UpdateOrderCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(typeof(OrderVm), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<OrderVm>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);

            return Ok(order);

        }


        [HttpGet("paginationByUsername", Name = "PaginationOrderByUsername")]
        [ProducesResponseType(typeof(PaginationVm<OrderVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<PaginationVm<OrderVm>>> PaginationOrderByUsername([FromQuery] PaginationOrdersQuery request)
        {
            request.Username = _authService.GetSessionUser();
            var orders = await _mediator.Send(request);

            return Ok(orders);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationOrder")]
        [ProducesResponseType(typeof(PaginationVm<OrderVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<PaginationVm<OrderVm>>> PaginationOrder([FromQuery] PaginationOrdersQuery request)
        {
            var orders = await _mediator.Send(request);

            return Ok(orders);
        }

    }
}