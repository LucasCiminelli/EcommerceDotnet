using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {
        private readonly UserManager<Usuario> _userManager;

        public ResetPasswordByTokenCommandHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword))
            {
                throw new BadRequestException("Los passwords ingresados no coinciden");
            }

            var usuario = await _userManager.FindByEmailAsync(request.Email!);

            if (usuario is null)
            {
                throw new BadRequestException("El email ingresado no está registrado como usuario");
            }

            var token = Convert.FromBase64String(request.Token!);
            var tokenResult = Encoding.UTF8.GetString(token);

            var result = await _userManager.ResetPasswordAsync(usuario, tokenResult, request.Password!);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo resetear el password");
            }

            return $"Se actualizó exitosamente el password para el email: {request.Email}";

        }
    }
}