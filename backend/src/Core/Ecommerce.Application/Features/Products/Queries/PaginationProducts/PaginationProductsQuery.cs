using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductsQuery : PaginationBaseQuery, IRequest<PaginationVm<ProductVm>>
    {
        public int? CategoryId { get; set; }
        public decimal? PrecioMax { get; set; }
        public decimal? PrecioMin { get; set; }
        public int? Rating { get; set; }
        public ProductStatus? Status { get; set; }
    }
}