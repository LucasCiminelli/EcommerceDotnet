using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Orders.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderVm>
    {

        public int OrderId { get; set; }
        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId == 0 ? throw new ArgumentNullException(nameof(orderId)) : orderId;
        }
    }
}