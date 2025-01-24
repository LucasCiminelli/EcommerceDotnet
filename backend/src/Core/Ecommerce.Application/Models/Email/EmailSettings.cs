using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.Email
{
    public class EmailSettings
    {
        public string? Email { get; set; }
        public string? Key { get; set; }
        public string? BaseUrlClient { get; set; }
    }
}