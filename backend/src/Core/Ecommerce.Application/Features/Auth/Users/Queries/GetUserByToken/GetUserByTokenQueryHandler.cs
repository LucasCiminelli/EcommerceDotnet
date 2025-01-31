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

namespace Ecommerce.Application.Features.Auth.Users.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {

        private readonly IAuthService _authService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByTokenQueryHandler(IAuthService authService, UserManager<Usuario> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authService = authService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new Exception("El usuario no está autenticado");
            }

            if (!user.isActive)
            {
                throw new Exception("El usuario está bloqueado, contacte al admin");
            }

            var direccionEnvio = await _unitOfWork.Repository<Address>().GetEntityAsync(x => x.Username == user.UserName);

            var roles = await _userManager.GetRolesAsync(user);
            var token = _authService.CreateToken(user, roles);
            var direccionEnvioMapped = _mapper.Map<AdressVm>(direccionEnvio);

            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                Username = user.UserName,
                Roles = roles,
                Avatar = user.AvatarUrl,
                DireccionEnvio = direccionEnvioMapped,
                Token = token
            };

        }
    }
}