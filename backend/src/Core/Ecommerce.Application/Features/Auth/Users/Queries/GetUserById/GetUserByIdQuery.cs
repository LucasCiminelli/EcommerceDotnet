using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Auth.Users.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<AuthResponse>
    {
        public string? Id { get; set; }

        public GetUserByIdQuery(string? id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}