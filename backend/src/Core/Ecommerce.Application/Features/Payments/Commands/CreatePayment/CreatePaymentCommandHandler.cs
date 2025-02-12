using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OrderVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var orderToPay = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x => x.Id == request.OrderId,
                null,
                false
            );

            if (orderToPay is null)
            {
                throw new Exception("No existe la orden de compra que quiere pagar");
            }

            orderToPay.Status = OrderStatus.Completed;

            _unitOfWork.Repository<Order>().UpdateEntity(orderToPay);

            var shoppingCartItems = await _unitOfWork.Repository<ShoppingCartItem>().GetAsync(
                x => x.ShoppingCartMasterId == request.ShoppingCartMasterId
            );

            _unitOfWork.Repository<ShoppingCartItem>().DeleteRange(shoppingCartItems);

            await _unitOfWork.Complete();

            var mappedOrder = _mapper.Map<OrderVm>(orderToPay);


            return mappedOrder;

        }
    }
}