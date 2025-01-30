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

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            user.Nombre = request.Nombre;
            user.Apellido = request.Apellido;
            user.Telefono = request.Telefono;
            user.AvatarUrl = request.FotoUrl ?? user.AvatarUrl;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar al usuario");
            }

            var updatedUser = await _userManager.FindByEmailAsync(request.Email!);

            if (updatedUser is null)
            {
                throw new Exception("No se encontró al usuario");
            }

            var roles = await _userManager.GetRolesAsync(updatedUser!);

            return new AuthResponse
            {
                Id = updatedUser.Id,
                Nombre = updatedUser.Nombre,
                Apellido = updatedUser.Apellido,
                Telefono = updatedUser.Telefono,
                Email = updatedUser.Email,
                Username = updatedUser.UserName,
                Avatar = updatedUser.AvatarUrl,
                Token = _authService.CreateToken(updatedUser, roles),
                Roles = roles
            };
        }
    }
}