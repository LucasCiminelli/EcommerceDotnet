using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Orders;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrders
{
    public class PaginationOrdersQueryHandler : IRequestHandler<PaginationOrdersQuery, PaginationVm<OrderVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<OrderVm>> Handle(PaginationOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderSpecificationParams = new OrderSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                OrderId = request.OrderId,
                Username = request.Username
            };

            var orderSpecification = new OrderSpecification(orderSpecificationParams);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpec(orderSpecification);

            var orderSpecCount = new OrderForCountingSpecification(orderSpecificationParams);
            var totalOrders = await _unitOfWork.Repository<Order>().CountAsync(orderSpecCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalOrders) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<OrderVm>>(orders);

            var ordersByPage = orders.Count();

            var pagination = new PaginationVm<OrderVm>
            {
                Data = data,
                Count = totalOrders,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = ordersByPage,
            };

            return pagination;
        }
    }
}