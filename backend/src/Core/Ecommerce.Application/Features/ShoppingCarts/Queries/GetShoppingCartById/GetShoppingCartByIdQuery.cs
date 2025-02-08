using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartVm>
    {
        public Guid ShoppingCartId { get; set; }


        public GetShoppingCartByIdQuery(Guid? shoppingCartId)
        {
            ShoppingCartId = shoppingCartId ?? throw new ArgumentNullException(nameof(shoppingCartId));
        }
    }
}