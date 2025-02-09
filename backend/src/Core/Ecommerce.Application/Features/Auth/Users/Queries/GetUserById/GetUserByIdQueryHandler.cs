using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auth.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<Usuario> userManager, IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id!);
            var username = _authService.GetSessionUser();

            var direccionEnvio = await _unitOfWork.Repository<Address>().GetEntityAsync(x => x.Username == username);

            var mappedDireccionEnvio = _mapper.Map<AddressVm>(direccionEnvio);

            if (user is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            var roles = await _userManager.GetRolesAsync(user);


            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Username = user.UserName,
                Telefono = user.Telefono,
                Avatar = user.AvatarUrl,
                Roles = roles,
                DireccionEnvio = mappedDireccionEnvio
            };
        }
    }
}