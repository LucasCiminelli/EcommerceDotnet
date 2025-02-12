using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Orders
{
    public class OrderSpecification : BaseSpecification<Order>
    {

        public OrderSpecification(OrderSpecificationParams orderParams) : base(
            x =>
            (string.IsNullOrEmpty(orderParams.Username) || x.CompradorUsername!.Contains(orderParams.Username))
            &&
            (!orderParams.OrderId.HasValue || x.Id == orderParams.OrderId)
        )
        {
            AddInclude(x => x.OrderItems!);
            ApplyPaging(orderParams.PageSize * (orderParams.PageIndex - 1), orderParams.PageSize);

            if (!string.IsNullOrEmpty(orderParams.Sort))
            {
                switch (orderParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(x => x.CreatedDate!);
                        break;
                    case "createDateDesc":
                        AddOrderByDescending(x => x.CreatedDate!);
                        break;
                    default:
                        AddOrderBy(x => x.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(x => x.CreatedDate!);
            }
        }
    }
}