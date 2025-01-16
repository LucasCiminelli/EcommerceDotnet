using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class ShoppingCartItem : BaseDomainModel
    {

        public string? Producto { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Imagen { get; set; }
        public string? Categoria { get; set; }
        public Guid ShoppingCartMasterId { get; set; }
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
    }
}