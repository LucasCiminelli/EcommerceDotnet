using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, Usuario>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateAdminUserCommandHandler(UserManager<Usuario> userManager, IAuthService authService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _authService = authService;
            _roleManager = roleManager;
        }

        public async Task<Usuario> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var userToEdit = await _userManager.FindByIdAsync(request.Id!);

            if (userToEdit is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            userToEdit.Nombre = request.Nombre;
            userToEdit.Apellido = request.Apellido;
            userToEdit.Telefono = request.Telefono;

            var resultUpdate = await _userManager.UpdateAsync(userToEdit);

            if (!resultUpdate.Succeeded)
            {
                throw new Exception($"No se pudo actualizar el usuario {userToEdit.UserName}");
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);

            if (role is null)
            {
                throw new Exception("El rol asignado no existe");
            }


            await _userManager.AddToRoleAsync(userToEdit, role.Name!);
            

            return userToEdit;

        }
    }
}