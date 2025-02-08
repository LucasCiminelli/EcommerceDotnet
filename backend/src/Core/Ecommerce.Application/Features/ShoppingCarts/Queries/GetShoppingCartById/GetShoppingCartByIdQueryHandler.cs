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

namespace Ecommerce.Application.Features.ShoppingCarts.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQueryHandler : IRequestHandler<GetShoppingCartByIdQuery, ShoppingCartVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCartVm> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {

            var includes = new List<Expression<Func<ShoppingCart, object>>>();

            includes.Add(x => x.ShoppingCartItems!.OrderBy(x => x.Producto));


            var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartId,
                includes,
                true
            );


            if (shoppingCart is null)
            {
                shoppingCart = new ShoppingCart
                {
                    ShoppingCartMasterId = request.ShoppingCartId,
                    ShoppingCartItems = new List<ShoppingCartItem>()

                };

                _unitOfWork.Repository<ShoppingCart>().AddEntity(shoppingCart);
                await _unitOfWork.Complete();
            }

            var mappedShoppingCart = _mapper.Map<ShoppingCartVm>(shoppingCart);


            return mappedShoppingCart;

        }
    }
}