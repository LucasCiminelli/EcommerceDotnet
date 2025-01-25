using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Auth.Users.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}