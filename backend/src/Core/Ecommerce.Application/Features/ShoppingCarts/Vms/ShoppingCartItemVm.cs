using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ShoppingCarts.Vms
{
    public class ShoppingCartItemVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Producto { get; set; }
        public string? Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Categoria { get; set; }
        public int Stock { get; set; }
        public decimal TotalLine
        {
            get
            {
                return Math.Round(Cantidad * Precio, 2);
            }

            set { }
        }

    }
}