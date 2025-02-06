using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewToDelete = await _unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId);

            if (reviewToDelete is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            await _unitOfWork.Repository<Review>().DeleteAsync(reviewToDelete);
            

            return Unit.Value;

        }
    }
}