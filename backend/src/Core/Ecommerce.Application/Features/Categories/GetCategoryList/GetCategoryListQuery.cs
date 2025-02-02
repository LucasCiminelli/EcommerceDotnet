using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Categories.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Categories.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {

    }
}