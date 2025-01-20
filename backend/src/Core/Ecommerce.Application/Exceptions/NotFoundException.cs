using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) no fue encontrado")
        {

        }
    }
}