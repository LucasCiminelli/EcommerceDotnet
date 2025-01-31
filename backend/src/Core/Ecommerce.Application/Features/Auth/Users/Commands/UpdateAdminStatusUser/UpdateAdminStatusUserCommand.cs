using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommand : IRequest<Usuario>
    {
        public string? Id { get; set; }
    }
}