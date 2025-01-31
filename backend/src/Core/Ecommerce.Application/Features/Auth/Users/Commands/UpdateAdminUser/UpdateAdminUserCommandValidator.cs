using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {

        public UpdateAdminUserCommandValidator()
        {
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El Nombre no puede estar vacío");

            RuleFor(p => p.Apellido)
            .NotEmpty().WithMessage("El Apellido no puede estar vacío");

            RuleFor(p => p.Telefono)
            .NotEmpty().WithMessage("El telefono no puede estar vacío");

        }
    }
}