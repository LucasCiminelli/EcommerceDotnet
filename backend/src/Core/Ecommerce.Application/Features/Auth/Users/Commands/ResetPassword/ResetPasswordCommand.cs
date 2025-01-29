using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}