using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {

        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Direccion)
            .NotEmpty()
            .WithMessage("La dirección no puede ser nula");

            RuleFor(x => x.Ciudad)
           .NotEmpty()
           .WithMessage("La ciudad no puede ser nula");

            RuleFor(x => x.Departamento)
           .NotEmpty()
           .WithMessage("El departamento no puede ser nula");

            RuleFor(x => x.CodigoPostal)
           .NotEmpty()
           .WithMessage("El codigo postal no puede ser nula");

            RuleFor(x => x.Pais)
           .NotEmpty()
           .WithMessage("El pais no puede ser nula");
        }

    }
}