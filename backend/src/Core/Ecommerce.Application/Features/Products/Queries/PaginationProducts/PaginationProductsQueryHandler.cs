using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductsQueryHandler : IRequestHandler<PaginationProductsQuery, PaginationVm<ProductVm>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public PaginationProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PaginationVm<ProductVm>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}