using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductVm>
    {
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? Vendedor { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }

        public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }

    }
}