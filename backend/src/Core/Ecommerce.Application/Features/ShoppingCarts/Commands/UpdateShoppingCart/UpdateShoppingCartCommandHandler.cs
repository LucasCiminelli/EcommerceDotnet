using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartToUpdate = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(x => x.ShoppingCartMasterId == request.ShoppingCartId);

            if (shoppingCartToUpdate is null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId!);
            }

            var shoppignCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(x => x.ShoppingCartMasterId == request.ShoppingCartId);

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppignCartItems); //eliminar todo el conjunto de items que existian previamente asociados al Id que estamos pasando en el request.

            var shoppingCartItemsToAdd = _mapper.Map<List<ShoppingCartItem>>(request.ShoppingCartItems);


            shoppingCartItemsToAdd.ForEach(x =>
            {
                x.ShoppingCartId = shoppingCartToUpdate.Id;
                x.ShoppingCartMasterId = request.ShoppingCartId;
            });

            _unitOfWork.Repository<ShoppingCartItem>().AddRange(shoppingCartItemsToAdd); //agregar nuevamente la lista actualizada con los items al ShoppingCart con el id que viene en el request.

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo modificar el carrito");
            }

            var includes = new List<Expression<Func<ShoppingCart, object>>>();

            includes.Add(x => x.ShoppingCartItems!.OrderBy(x => x.Producto));

            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes,
                true
                );

            var mappedShoppingCart = _mapper.Map<ShoppingCartVm>(shoppingCart);


            return mappedShoppingCart;

        }
    }
}