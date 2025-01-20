using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Ecommerce.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }


        public ValidationException() : base("Se presentaron uno o mas errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

    }
}