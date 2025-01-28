using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.SendPassword
{
    public class SendPasswordCommand : IRequest<string>
    {
        public string? Email { get; set; }
    }
}