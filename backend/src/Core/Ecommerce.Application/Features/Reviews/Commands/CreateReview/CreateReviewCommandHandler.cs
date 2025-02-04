using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewEntity = new Review
            {
                Comentario = request.Comentario,
                Nombre = request.Nombre,
                ProductId = request.ProductId,
                Rating = request.Rating
            };

            _unitOfWork.Repository<Review>().AddEntity(reviewEntity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo guardar el comentario");
            }

            var mappedReview = _mapper.Map<ReviewVm>(reviewEntity);


            return mappedReview;
        }
    }
}