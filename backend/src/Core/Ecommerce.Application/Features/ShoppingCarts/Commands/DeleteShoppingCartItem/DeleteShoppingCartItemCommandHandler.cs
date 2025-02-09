using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommandHandler : IRequestHandler<DeleteShoppingCartItemCommand, ShoppingCartVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShoppingCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartItemToDelete = await _unitOfWork.Repository<ShoppingCartItem>().GetEntityAsync(x => x.Id == request.ShoppingCartItemId);

            if (shoppingCartItemToDelete is null)
            {
                throw new ArgumentNullException(nameof(shoppingCartItemToDelete));
            }


            await _unitOfWork.Repository<ShoppingCartItem>().DeleteAsync(shoppingCartItemToDelete);


            var includes = new List<Expression<Func<ShoppingCart, object>>>();
            includes.Add(x => x.ShoppingCartItems!.OrderBy(y => y.Producto));


            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(

                x => x.ShoppingCartMasterId == shoppingCartItemToDelete.ShoppingCartMasterId,
                includes,
                true

                );

            var mappedShoppingCart = _mapper.Map<ShoppingCartVm>(shoppingCart);


            return mappedShoppingCart;
        }
    }
}