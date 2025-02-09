using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommand : IRequest<ShoppingCartVm>
    {

        public int ShoppingCartItemId { get; set; }

        public DeleteShoppingCartItemCommand(int shoppingCartItemId)
        {
            ShoppingCartItemId = shoppingCartItemId == 0 ? throw new ArgumentNullException(nameof(shoppingCartItemId)) : shoppingCartItemId;
        }
    }
}