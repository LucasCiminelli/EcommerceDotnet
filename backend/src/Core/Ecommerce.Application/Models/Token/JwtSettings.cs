using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.Token
{
    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public TimeSpan ExpireTime { get; set; }
    }
}