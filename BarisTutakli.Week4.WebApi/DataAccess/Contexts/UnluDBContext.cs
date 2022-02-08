using BarisTutakli.Week4.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Contexts
{
    public class UnluDBContext : IdentityDbContext<User>
    {
        public UnluDBContext(DbContextOptions<UnluDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
       DbSet<Product> Products { get; set; }
    }
}
