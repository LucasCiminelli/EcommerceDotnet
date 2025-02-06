using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Reviews;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Queries.PaginationReviews
{
    public class PaginationReviewsQueryHandler : IRequestHandler<PaginationReviewsQuery, PaginationVm<ReviewVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<ReviewVm>> Handle(PaginationReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviewSpecificationParams = new ReviewSpecificationParams
            {
                ProductId = request.ProductId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
            };

            var spec = new ReviewSpecification(reviewSpecificationParams);

            var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpec(spec);

            var specCount = new ReviewForCountingSpecification(reviewSpecificationParams);
            var totalReviews = await _unitOfWork.Repository<Review>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalReviews) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewVm>>(reviews);
            var reviewsByPage = reviews.Count();


            var pagination = new PaginationVm<ReviewVm>
            {
                Count = totalReviews,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = reviewsByPage
            };


            return pagination;
        }
    }
}