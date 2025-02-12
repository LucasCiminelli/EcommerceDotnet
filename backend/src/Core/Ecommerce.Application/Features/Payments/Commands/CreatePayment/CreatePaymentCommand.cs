using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Orders.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<OrderVm>
    {
        public int OrderId { get; set; }
        public Guid? ShoppingCartMasterId { get; set; }
    }
}