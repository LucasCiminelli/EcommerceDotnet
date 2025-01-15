using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Order : BaseDomainModel
    {

        public Order()
        {

        }

        public Order(
            string compradornombre,
            string compradorEmail,
            OrderAdress orderAdress,
            decimal subtotal,
            decimal total,
            decimal impuesto,
            decimal precioEnvio)
        {
            CompradorNombre = compradornombre;
            CompradorUsername = compradorEmail;
            OrderAdress = orderAdress;
            Subtotal = subtotal;
            Total = total;
            Impuesto = impuesto;
            PrecioEnvio = precioEnvio;
        }

        public string? CompradorNombre { get; set; }
        public string? CompradorUsername { get; set; }
        public OrderAdress? OrderAdress { get; set; }
        public IReadOnlyList<OrderItem>? OrderItems { get; set; }


        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Subtotal { get; set; }
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;


        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Total { get; set; }


        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Impuesto { get; set; }


        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal PrecioEnvio { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public string? StripeApiKey { get; set; }

    }
}