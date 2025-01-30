using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre no puede ser nulo");

            RuleFor(p => p.Apellido)
            .NotEmpty().WithMessage("El apellido no puede ser nulo");

        }
    }
}