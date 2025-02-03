using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>()
            .ForMember(p => p.CategoryNombre, x => x.MapFrom(a => a.Category!.Nombre))
            .ForMember(p => p.NumeroReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));

            CreateMap<Image, ImageVm>();
            CreateMap<Review, ReviewVm>();
            CreateMap<Country, CountryVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageCommand, Image>();
        }
    }
}