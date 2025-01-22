using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Ecommerce.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; } //cada excepción tiene un key(nombre) y puede tener un conjunto de validaciones por cada key


        public ValidationException() : base("Se presentaron uno o mas errores de validación") //llamarlo sin argumentos
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this() //recibe una coleccion de errores de validacion
        {
            Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage) //que los errores se agrupen por nombre y la descripción
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()); //parseado a Dictionary
        }

    }
}