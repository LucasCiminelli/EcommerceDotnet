using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auth.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager, IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);

            if (user == null)
            {
                throw new NotFoundException(nameof(Usuario), request.Email!);
            }

            if (!user.isActive)
            {
                throw new Exception($"El usuario {user.UserName} está bloqueado");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

            if (!result.Succeeded)
            {
                throw new Exception("Las credenciales del usuario son erronéas");
            }

            var direccionUsuario = await _unitOfWork.Repository<Address>().GetEntityAsync(x => x.Username == user.UserName);

            var roles = await _userManager.GetRolesAsync(user);

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                DireccionEnvio = _mapper.Map<AdressVm>(direccionUsuario),
                Token = _authService.CreateToken(user, roles),
                Roles = roles
            };

            return authResponse;
        }
    }
}