using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();
        string CreateToken(Usuario usuario, IList<string>? roles);
    }
}