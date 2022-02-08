using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Models
{
    public class TokenView
    {
        public string Token { get; set; }
        public DateTime expiration { get; set; }
    }
}
