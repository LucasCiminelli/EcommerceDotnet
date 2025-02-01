using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Users
{
    public class UserSpecification : BaseSpecification<Usuario>
    {

        public UserSpecification(UserSpecificationParams userParams): base(
            x => (
            string.IsNullOrEmpty(userParams.Search) || x.Nombre!.Contains(userParams.Search) || x.UserName!.Contains(userParams.Search)
            || x.Apellido!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
            )
        )
        {


            ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(x => x.Nombre!);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(x => x.Nombre!);
                        break;
                    case "ApellidoAsc":
                        AddOrderBy(x => x.Apellido!);
                        break;
                    case "apellidoDesc":
                        AddOrderByDescending(x => x.Apellido!);
                        break;
                    default:
                        AddOrderBy(x => x.Nombre!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(x => x.Nombre!);
            }

        }


    }
}