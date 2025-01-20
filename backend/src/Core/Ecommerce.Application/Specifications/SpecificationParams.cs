using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Specifications
{
    public class SpecificationParams
    {
        public string? Sort { get; set; }
        public int PageIndex { get; set; }
        private const int MaxPageSize = 50;
        private int _pageSize = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }
    }
}