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

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productSelected = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (productSelected is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productSelected, typeof(UpdateProductCommand), typeof(Product)); //source, destination, type source, type destination

            await _unitOfWork.Repository<Product>().UpdateAsync(productSelected);

            if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
            {
                var imagesToRemove = await _unitOfWork.Repository<Image>().GetAsync(x => x.ProductId == request.Id);

                //Eliminando las imagenes anteriores relacionadas al producto
                _unitOfWork.Repository<Image>().DeleteRange(imagesToRemove);

                //asignandole a las nuevas imagenes el productId que proviene en el RequestId, convirtiendolas a un tipo Image para almacenarlas en la BD y agregandolas a la BD.

                request.ImageUrls.Select(x => { x.ProductId = request.Id; return x; }).ToList(); //Actualizar el productId al cual estan relacionadas las imagenes con el dato del productId que proviene desde el requestId del producto. Actualizando para cada objeto imagen el productId
                var images = _mapper.Map<List<Image>>(request.ImageUrls);
                _unitOfWork.Repository<Image>().AddRange(images);

                await _unitOfWork.Complete();
            }


            var finalUpdatedProduct = _mapper.Map<ProductVm>(productSelected);

            
            return finalUpdatedProduct;

        }
    }
}