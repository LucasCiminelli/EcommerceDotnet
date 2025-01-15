using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Country : BaseDomainModel
    {
        public string? Nombre { get; set; }
        public string? Iso2 { get; set; }
        public string? Iso3 { get; set; }

    }
}