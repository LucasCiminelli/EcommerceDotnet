using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ShoppingCarts.Vms
{
    public class ShoppingCartVm
    {
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItemVm>? ShoppingCartItems { get; set; }
        public decimal Total
        {

            get
            {
                return Math.Round(
                    ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad) +
                    ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad) * Convert.ToDecimal(0.18) +
                    ((ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25), 2
                );
            }
            set { }

        }

        public int Cantidad
        {
            get
            {
                return ShoppingCartItems!.Sum(x => x.Cantidad);
            }
            set { }
        }

        public decimal Subtotal
        {
            get
            {
                return Math.Round(ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad), 2);
            }
            set { }
        }

        public decimal Impuesto
        {
            get
            {
                return Math.Round(((ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) * Convert.ToDecimal(0.18)), 2);
            }
            set { }
        }

        public decimal PrecioEnvio
        {
            get
            {
                return (ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25;
            }
            set { }
        }

    }
}