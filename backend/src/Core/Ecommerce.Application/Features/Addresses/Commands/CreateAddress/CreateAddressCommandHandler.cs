using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressVm>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddressVm> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {

            var username = _authService.GetSessionUser();

            var addressRecord = await _unitOfWork.Repository<Address>().GetEntityAsync(
                x => x.Username == username,
                null,
                false
            );

            if (addressRecord is null)
            {
                addressRecord = new Address
                {
                    Direccion = request.Direccion,
                    Ciudad = request.Ciudad,
                    Departamento = request.Departamento,
                    CodigoPostal = request.CodigoPostal,
                    Pais = request.Pais,
                    Username = username
                };

                _unitOfWork.Repository<Address>().AddEntity(addressRecord);

            }
            else
            {
                addressRecord.Direccion = request.Direccion;
                addressRecord.Ciudad = request.Ciudad;
                addressRecord.Departamento = request.Departamento;
                addressRecord.CodigoPostal = request.CodigoPostal;
                addressRecord.Pais = request.Pais;
            }

            await _unitOfWork.Complete();


            var mappedAddress = _mapper.Map<AddressVm>(addressRecord);


            return mappedAddress;
        }
    }
}