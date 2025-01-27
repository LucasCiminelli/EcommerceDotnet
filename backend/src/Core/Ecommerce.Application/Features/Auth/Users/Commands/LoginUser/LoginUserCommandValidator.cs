using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ecommerce.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email no puede ser nulo");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("El password no puede ser nulo");


        }
    }
}