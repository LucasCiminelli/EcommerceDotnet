using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auth.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auth.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auth.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auth.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auth.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auth.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Features.Auth.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auth.Users.Vms;
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


        [HttpPost("updatepassword", Name = "UpdatePassword")]
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

    }
}