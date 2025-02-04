using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductVm>
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? Vendedor { get; set; }
        public int Stock { get; set; }
        public string? CateogoryId { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }
        public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }

    }
}