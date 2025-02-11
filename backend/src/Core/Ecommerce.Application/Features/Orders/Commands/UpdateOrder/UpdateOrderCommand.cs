using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderVm>
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}