using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auth.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(UserManager<Usuario> userManager, IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username!);
            var username = _authService.GetSessionUser();

            var direccionEnvio = await _unitOfWork.Repository<Address>().GetEntityAsync(x => x.Username == username);
            var mappedDireccionEnvio = _mapper.Map<AddressVm>(direccionEnvio);

            if (user is null)
            {
                throw new Exception("El usuario no existe");
            }

            var roles = await _userManager.GetRolesAsync(user);


            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = roles,
                DireccionEnvio = mappedDireccionEnvio
            };

        }
    }
}