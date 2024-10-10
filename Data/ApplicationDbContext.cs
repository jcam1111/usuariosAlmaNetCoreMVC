// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using UsuariosAlmaNetCoreMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UsuariosAlmaNetCoreMVC.Data
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes hacer configuraciones adicionales si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }

}
