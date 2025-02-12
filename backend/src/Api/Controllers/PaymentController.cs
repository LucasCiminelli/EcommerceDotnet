using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(Name = "CreatePayment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<OrderVm>> CreatePayment([FromBody] CreatePaymentCommand request)
        {

            return await _mediator.Send(request);
        }

    }
}