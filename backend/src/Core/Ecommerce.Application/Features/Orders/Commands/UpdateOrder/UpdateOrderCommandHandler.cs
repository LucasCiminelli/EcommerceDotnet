using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);

            if (order is null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            order.Status = request.Status;

            _unitOfWork.Repository<Order>().UpdateEntity(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo actualizar el status de la orden de compra");
            }

            var orderMapped = _mapper.Map<OrderVm>(order);


            return orderMapped;
        }
    }
}