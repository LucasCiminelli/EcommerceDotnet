using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Models.Order;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.Orders.Vms
{
    public class OrderVm
    {
        public int Id { get; set; }
        public AddressVm? OrderAddress { get; set; }
        public List<OrderItemVm>? OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioEnvio { get; set; }
        public OrderStatus Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public string? StripeApiKey { get; set; }
        public string? CompradorUsername { get; set; }
        public string? CompradorNombre { get; set; }
        public int Cantidad
        {
            get { return OrderItems!.Sum(x => x.Cantidad); }
            set { }
        }

        public string? StatusLabel
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.Completed:
                        return OrderStatusLabel.COMPLETED;
                    case OrderStatus.Enviado:
                        return OrderStatusLabel.ENVIADO;
                    case OrderStatus.Pending:
                        return OrderStatusLabel.PENDING;
                    case OrderStatus.Error:
                        return OrderStatusLabel.ERROR;
                    default: return OrderStatusLabel.ERROR;

                }
            }
            set { }
        }
    }
}