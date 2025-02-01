using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>>
    {
            
    }
}