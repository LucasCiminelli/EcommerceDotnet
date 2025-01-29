using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommand : IRequest<string>
    {

        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

    }
}