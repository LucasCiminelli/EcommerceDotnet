using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auth.Roles.Queries.GetRoles;
using Ecommerce.Application.Features.Auth.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auth.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auth.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auth.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auth.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminStatusUser;
using Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Features.Auth.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auth.Users.GetUserByToken;
using Ecommerce.Application.Features.Auth.Users.Queries.GetUserById;
using Ecommerce.Application.Features.Auth.Users.Queries.GetUserByUsername;
using Ecommerce.Application.Features.Auth.Users.Queries.PaginationUsers;
using Ecommerce.Application.Features.Auth.Users.Vms;
using Ecommerce.Application.Features.Shared.Queries.Vms;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IManageImageService _manageImageService;

        public UsuarioController(IMediator mediator, IManageImageService manageImageService)
        {
            _mediator = mediator;
            _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
        {
            return await _mediator.Send(request);
        }



        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
        {
            if (request.Foto is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Foto!.OpenReadStream(),
                    Nombre = request.Foto.Name
                });

                request.FotoId = resultImage.PublicId;
                request.FotoUrl = resultImage.Url;

            }


            return await _mediator.Send(request);

        }


        [AllowAnonymous]
        [HttpPost("forgotpassword", Name = "ForgotPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
        {

            return await _mediator.Send(request);

        }

        [AllowAnonymous]
        [HttpPost("resetpassword", Name = "ResetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPut("updatepassword", Name = "UpdatePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPut("update", Name = "Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
        {
            if (request.Foto is not null)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = request.Foto.OpenReadStream(),
                    Nombre = request.Foto.Name,
                });

                request.FotoId = resultImage.PublicId;
                request.FotoUrl = resultImage.Url;
            }

            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Usuario>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Usuario>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<AuthResponse>> GetUserById(string id)
        {

            var query = new GetUserByIdQuery(id);

            return await _mediator.Send(query);
        }


        [HttpGet("", Name = "CurrentUser")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<AuthResponse>> CurrentUser()
        {

            var query = new GetUserByTokenQuery();

            return await _mediator.Send(query);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("username/{username}", Name = "GetUserByUsername")]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<AuthResponse>> GetUserByUsername(string username)
        {

            var query = new GetUserByUsernameQuery(username);

            return await _mediator.Send(query);
        }

        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("paginationAdmin", Name = "PaginationUser")]
        [ProducesResponseType(typeof(PaginationVm<Usuario>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<PaginationVm<Usuario>>> PaginationUser([FromQuery] PaginationUsersQuery paginationUsersQuery)
        {

            var paginationUser = await _mediator.Send(paginationUsersQuery);

            return Ok(paginationUser);

        }

        [AllowAnonymous]
        [HttpGet("roles", Name = "GetRolesList")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<string>>> GetRolesList()
        {
            var query = new GetRolesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}