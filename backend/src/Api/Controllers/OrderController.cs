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
using Ecommerce.Application.Features.Orders.Vms;
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

    }
}