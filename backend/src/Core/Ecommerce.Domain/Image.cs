using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Image : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public string? PublicCode { get; set; }
    }
}