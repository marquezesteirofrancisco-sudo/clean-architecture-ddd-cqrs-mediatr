using CleanArchitectureTemplate.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Agregamos todas las Entities o Modelos con DbSet 

        public DbSet<Product> Productos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
