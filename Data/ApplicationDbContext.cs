// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace UsuariosAlmaNetCoreMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }

}
