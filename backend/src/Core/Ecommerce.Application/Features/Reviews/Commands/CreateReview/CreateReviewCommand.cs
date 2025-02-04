using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<ReviewVm>
    {
        public int ProductId { get; set; }
        public string? Nombre { get; set; }
        public string? Comentario { get; set; }
        public int Rating { get; set; }
    }
}