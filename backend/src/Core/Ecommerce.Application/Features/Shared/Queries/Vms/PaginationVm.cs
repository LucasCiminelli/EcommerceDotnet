using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Shared.Queries.Vms
{
    public class PaginationVm<T> where T : class
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T>? Data { get; set; }
        public int PageCount { get; set; }
        public int ResultByPage { get; set; }
    }
}