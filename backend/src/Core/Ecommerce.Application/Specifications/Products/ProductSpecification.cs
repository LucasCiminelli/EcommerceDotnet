using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification(ProductSpecificationParams productParams)
        : base(
            x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Nombre!.Contains(productParams.Search) || x.Descripcion!.Contains(productParams.Search))
            &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
            &&
            (!productParams.PrecioMin.HasValue || x.Precio >= productParams.PrecioMin)
            &&
            (!productParams.PrecioMax.HasValue || x.Precio <= productParams.PrecioMax)
            &&
            (!productParams.Status.HasValue || x.Status == productParams.Status)
        )
        {

            AddInclude(p => p.Reviews!);
            AddInclude(p => p.Images!);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre!);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre!);
                        break;
                    case "precioAsc":
                        AddOrderBy(p => p.Precio!);
                        break;
                    case "precioDesc":
                        AddOrderByDescending(p => p.Precio!);
                        break;
                    case "ratingAsc":
                        AddOrderBy(p => p.Rating!);
                        break;
                    case "ratingDesc":
                        AddOrderBy(p => p.Rating!);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate);
            }


        }

    }
}