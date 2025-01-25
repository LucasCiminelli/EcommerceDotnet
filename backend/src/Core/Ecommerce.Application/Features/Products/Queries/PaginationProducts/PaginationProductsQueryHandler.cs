using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductsQueryHandler : IRequestHandler<PaginationProductsQuery, PaginationVm<ProductVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<ProductVm>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
        {

            var productSpecificationParams = new ProductSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                CategoryId = request.CategoryId,
                PrecioMax = request.PrecioMax,
                PrecioMin = request.PrecioMin,
                Rating = request.Rating,
                Status = request.Status
            };

            var spec = new ProductSpecification(productSpecificationParams);


            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);
            
            var spectCount = new ProductForCountingSpecification(productSpecificationParams);

            var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(spectCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<ProductVm>>(products);

            var productsByPage = products.Count();


            var paginationVm = new PaginationVm<ProductVm>
            {
                Count = totalProducts,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage
            };

            return paginationVm;

        }
    }
}