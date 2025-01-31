using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommand : IRequest<Usuario>
    {

        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Role { get; set; }

    }
}