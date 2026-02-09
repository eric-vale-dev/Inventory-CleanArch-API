using Microsoft.EntityFrameworkCore;
using Inventario.Domain;

namespace Inventario.Infrastructure.Persistence
{
    // Vamos a heredar de DbContext, que es la clase base para trabajar con Entity Framework Core
    public class ApplicationDbContext : DbContext
    {
        // Construcutor: recibe las opciones (como la cadena de conexión) y se la da a la clase base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        // DbSet representa una tabla en la base de datos
        // EF Core creará una tabla "Productos" basada en la clase producto
        public DbSet<Producto> Productos {get; set;}

        // Aquí podríamos configurar reglas especificas de la BD si se necesita 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Por ejemplo: configurar que el nombre sea obligatorio a nivel de base de datos
            // modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired();
        }
    }
}