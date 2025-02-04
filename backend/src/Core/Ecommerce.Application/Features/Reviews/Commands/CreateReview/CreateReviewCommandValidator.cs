using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {

        public CreateReviewCommandValidator()
        {
            RuleFor(c => c.Nombre)
            .NotNull()
            .WithMessage("El nombre no puede estar vacío");

            RuleFor(c => c.Comentario)
            .NotNull()
            .WithMessage("El comentario no puede estar vacío");

            RuleFor(c => c.Rating)
            .NotNull()
            .WithMessage("El rating no permite valores nulos");

        }
    }
}