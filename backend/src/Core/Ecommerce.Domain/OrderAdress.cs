using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class OrderAdress : BaseDomainModel
    {
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Username { get; set; }
        public string? Pais { get; set; }

    }
}