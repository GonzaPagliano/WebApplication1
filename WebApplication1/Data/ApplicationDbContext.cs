using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Rubro> Rubros { get; set; }

        public DbSet<WebApplication1.Models.Articulo> Articulos { get; set; }

        public DbSet<WebApplication1.Models.SubRubro> SubRubros { get; set; }
    }
}
