using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Reviews.Commands.CreateReview;
using Ecommerce.Application.Features.Reviews.Commands.DeleteReview;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(Name = "CreateReview")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReviewVm>> CreateReview([FromBody] CreateReviewCommand request)
        {
            return await _mediator.Send(request);
        }


        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete("{id}", Name = "DeleteReview")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> DeleteReview(int id)
        {
            var command = new DeleteReviewCommand(id);

            return await _mediator.Send(command);
        }

    }
}