using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Models.Email;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.SendPassword
{
    public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
    {

        private readonly IEmailService _emailService;
        private readonly UserManager<Usuario> _userManager;

        public SendPasswordCommandHandler(IEmailService emailService, UserManager<Usuario> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null)
            {
                throw new ArgumentException("No se ingresó un email");
            }

            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user is null)
            {
                throw new BadRequestException("No se encontró un usuario con el email ingresado");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var plainTextBytes = Encoding.UTF8.GetBytes(token); //extrayendo bytes del token

            token = Convert.ToBase64String(plainTextBytes); //codificado

            var emailMessage = new EmailMessage
            {
                To = request.Email,
                Body = "Resetear el password, dale click aqui:",
                Subject = "Cambiar el password"
            };

            var result = await _emailService.SendEmailAsync(emailMessage, token);

            if (!result)
            {
                throw new Exception("No se pudo enviar el email");
            }

            return $"Se envió el email a la cuenta {request.Email}";

        }
    }
}