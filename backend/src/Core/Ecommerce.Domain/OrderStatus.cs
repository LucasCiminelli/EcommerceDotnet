using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,
        [EnumMember(Value = "El pago fue recibido")]
        Completed,
        [EnumMember(Value = "El producto fue enviado al cliente")]
        Enviado,
        [EnumMember(Value = "El pago tuvo errores")]
        Error
    }
}