using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId); //nombre del tipo de dato, request.
            }

            product.Status = product.Status == ProductStatus.Inactivo ? ProductStatus.Activo : ProductStatus.Inactivo;

            await _unitOfWork.Repository<Product>().UpdateAsync(product);

            var mappedProduct = _mapper.Map<ProductVm>(product);


            return mappedProduct;
        }
    }
}