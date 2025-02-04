using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<ProductVm>
    {

        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId == 0 ? throw new ArgumentException(nameof(productId)) : productId;
        }
    }
}