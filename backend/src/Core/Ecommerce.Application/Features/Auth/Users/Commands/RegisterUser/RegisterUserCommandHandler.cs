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

namespace Ecommerce.Application.Features.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;


        public RegisterUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExistente = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;

            if (userExistente)
            {
                throw new BadRequestException("El Email del usuario ya existe en la base de datos");
            }

            var userExistenteByUsername = await _userManager.FindByNameAsync(request.Username!) is null ? false : true;

            if (userExistenteByUsername)
            {
                throw new BadRequestException("El Username del usuario ya existe en la base de datos");
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.Username,
                Email = request.Email,
                Telefono = request.Telefono,
                AvatarUrl = request.FotoUrl,

            };

            var result = await _userManager.CreateAsync(nuevoUsuario, request.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(nuevoUsuario, AppRole.GenericUser);
                var roles = await _userManager.GetRolesAsync(nuevoUsuario);

                return new AuthResponse
                {
                    Id = nuevoUsuario.Id,
                    Nombre = nuevoUsuario.Nombre,
                    Apellido = nuevoUsuario.Apellido,
                    Telefono = nuevoUsuario.Telefono,
                    Username = nuevoUsuario.UserName,
                    Email = nuevoUsuario.Email,
                    Avatar = nuevoUsuario.AvatarUrl,
                    Token = _authService.CreateToken(nuevoUsuario, roles),
                    Roles = roles
                };
            }

            throw new Exception("No se pudo registrar el usuario");

        }
    }
}