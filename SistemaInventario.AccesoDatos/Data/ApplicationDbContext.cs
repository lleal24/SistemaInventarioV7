using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.Modelos;
using System.Reflection;

namespace SistemaInventario.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }

        /// <summary>
        /// Sobre escribit metodo
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configuracion para Reflection
            // x cada modelo generar su respectivo archivo de configuracion
            builder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
        }
    }
}