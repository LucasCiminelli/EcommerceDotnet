using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Queries.PaginationUsers
{
    public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<Usuario>>
    {
        
    }
}