using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Categories.GetCategoryList;
using Ecommerce.Application.Features.Categories.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategories()
        {
            var query = new GetCategoryListQuery();

            var categoriesList = await _mediator.Send(query);

            return Ok(categoriesList);
        }



    }
}