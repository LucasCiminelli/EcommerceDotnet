using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auth.Users.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;

        public GetUserByIdQueryHandler(UserManager<Usuario> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id!);

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
            };
        }
    }
}