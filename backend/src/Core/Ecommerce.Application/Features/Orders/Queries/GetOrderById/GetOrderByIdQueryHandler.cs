using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {

            var includes = new List<Expression<Func<Order, object>>>();

            includes.Add(x => x.OrderItems!.OrderBy(y => y.ProductNombre));
            includes.Add(x => x.OrderAdress!);

            var order = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x => x.Id == request.OrderId,
                includes,
                false
            );

            if (order is null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var mappedOrder = _mapper.Map<OrderVm>(order);


            return mappedOrder;

        }
    }
}