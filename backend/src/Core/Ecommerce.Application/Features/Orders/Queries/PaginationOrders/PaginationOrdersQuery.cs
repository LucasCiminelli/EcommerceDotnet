using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrders
{
    public class PaginationOrdersQuery : PaginationBaseQuery, IRequest<PaginationVm<OrderVm>>
    {
        public string? Username { get; set; }
        public int? OrderId { get; set; }
    }
}