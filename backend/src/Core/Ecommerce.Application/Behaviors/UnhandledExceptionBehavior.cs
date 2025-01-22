using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next(); //seguir con el flujo normal
            }
            catch (Exception ex)
            {

                var requestName = typeof(TRequest).Name; //capturar el nombre del Request, normalmente clases definidas por CQRS.
                _logger.LogError(ex, "Application request: Sucedio una exception para el request {Name} {@Request}", requestName, request); //imprimir errores
                throw new Exception("Application request con Errores");

            }
        }
    }
}