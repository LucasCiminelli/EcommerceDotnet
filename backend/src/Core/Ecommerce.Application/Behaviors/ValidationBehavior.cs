using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Ecommerce.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        //recibe un TRequest, devuelve un TResponse, implementa IPipelineBehavior,
        // que recibe un TRequest y devuelve un TResponse donde el TRequest es de tipo IRequest parseando un TResponse
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators; //listado de Validaciones parseando los objetos de tipo TRequest (enviado por el cliente)


        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any()) //Si hay algun valor dentro de validators
            {

                var context = new ValidationContext<TRequest>(request); //obtener el contexto del Request

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                //Devuelve todas las validaciones que se ejecuten en este contexto sobre este Request

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList(); //Lista de errores resultantes de validationResults

                if (failures.Count != 0) //si hay errores devuelve un validationException con el listado de fallos
                {
                    throw new ValidationException(failures);
                }

            }

            return await next(); //sino sigue.

        }
    }
}