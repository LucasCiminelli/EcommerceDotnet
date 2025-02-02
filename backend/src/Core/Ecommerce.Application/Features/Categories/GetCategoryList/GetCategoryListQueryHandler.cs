using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Categories.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IReadOnlyList<CategoryVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().GetAsync(
                null,
                x => x.OrderBy(y => y.Nombre),
                string.Empty,
                false
            );

            var mappedCategories = _mapper.Map<IReadOnlyList<CategoryVm>>(categories);

            return mappedCategories;
        }
    }
}