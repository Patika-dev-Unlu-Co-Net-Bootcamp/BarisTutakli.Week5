using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Models
{
    public class RegisterAdminResponse
    {
        public IdentityResult Result { get; set; }
        public User User { get; set; }
    }
}
