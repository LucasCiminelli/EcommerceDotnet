using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Reviews.Queries.Vms
{
    public class ReviewVm
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Rating { get; set; }
        public string? Comentario { get; set; }
        public int ProductId { get; set; }
    }
}