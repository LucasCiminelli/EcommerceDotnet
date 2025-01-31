using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Auth.Users.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.GetUserByToken
{
    public class GetUserByTokenQuery : IRequest<AuthResponse>
    {

    }
}