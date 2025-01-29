using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(UserManager<Usuario> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            if (request.OldPassword is null)
            {
                throw new BadRequestException("No se ingresó el password antiguo");
            }

            var oldPassword = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.OldPassword!);


            if (!(oldPassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestException("El password ingresado es erroneo");
            }

            if (request.NewPassword is null)
            {
                throw new BadRequestException("No se ingresó un nuevo password");
            }

            var newPassword = _userManager.PasswordHasher.HashPassword(user, request.NewPassword!);

            user.PasswordHash = newPassword;
            var result = await _userManager.UpdateAsync(user);


            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar la password del usuario");
            }


            return Unit.Value;

        }
    }
}