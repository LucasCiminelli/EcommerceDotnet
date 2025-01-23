using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : class
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            if (specification.Criteria != null)
            {
                inputQuery = inputQuery.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderBy(specification.OrderByDescending);
            }

            if (specification.IsPagingEnable)
            {
                inputQuery = inputQuery.Skip(specification.Skip).Take(specification.Take); //ejemplo skip 100, take 20. osea desde la posición 100 dame 20 elementos.
            }

            inputQuery = specification.Includes!.Aggregate(inputQuery, (current, include) => current.Include(include)).AsSplitQuery().AsNoTracking();


            return inputQuery;
        }
    }
}