using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Reviews
{
    public class ReviewSpecification : BaseSpecification<Review>
    {

        public ReviewSpecification(ReviewSpecificationParams reviewParams)
        : base(
            x =>
            (!reviewParams.ProductId.HasValue || x.ProductId == reviewParams.ProductId)
        )
        {

            ApplyPaging(reviewParams.PageSize * (reviewParams.PageIndex - 1), reviewParams.PageSize);

            if (!string.IsNullOrEmpty(reviewParams.Sort))
            {
                switch (reviewParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(r => r.CreatedDate);
                        break;
                    case "createDateDesc":
                        AddOrderBy(r => r.CreatedDate);
                        break;
                    default:
                        AddOrderBy(r => r.CreatedDate);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(r => r.CreatedDate);
            }


        }


    }
}