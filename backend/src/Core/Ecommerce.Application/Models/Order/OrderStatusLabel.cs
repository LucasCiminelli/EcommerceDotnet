using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.Order
{
    public static class OrderStatusLabel
    {
        public const string? PENDING = nameof(PENDING);
        public const string? COMPLETED = nameof(COMPLETED);
        public const string? ENVIADO = nameof(ENVIADO);
        public const string? ERROR = nameof(ERROR);

    }
}