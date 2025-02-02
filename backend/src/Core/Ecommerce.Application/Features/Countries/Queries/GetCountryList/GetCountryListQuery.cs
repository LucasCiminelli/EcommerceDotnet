using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListQuery : IRequest<IReadOnlyList<CountryVm>> //List permite modificar y agregar data, IReadonlylist solo permite leer. IReadonlylist más rápido para obtener data de la consulta.
    {

    }
}