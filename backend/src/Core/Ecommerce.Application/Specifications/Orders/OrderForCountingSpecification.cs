using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Orders
{
    public class OrderForCountingSpecification : BaseSpecification<Order>
    {

        public OrderForCountingSpecification(OrderSpecificationParams orderParams) : base(
            x =>
            (string.IsNullOrEmpty(orderParams.Username) || x.CompradorUsername!.Contains(orderParams.Username))
            &&
            (!orderParams.OrderId.HasValue || x.Id == orderParams.OrderId)
        )
        {
        }
    }
}