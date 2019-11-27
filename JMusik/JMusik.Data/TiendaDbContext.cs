using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JMusik.Models;
using JMusik.Data.Configuracion;

namespace JMusik.Data
{
    public partial class TiendaDbContext : DbContext
    {
        public TiendaDbContext()
        {
        }

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetalleOrden> DetallesOrden { get; set; }
        public virtual DbSet<Orden> Ordenes { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.ApplyConfiguration(new DetallesOrdenConfiguracion());
            modelBuilder.ApplyConfiguration(new OrdenConfiguracion());
            modelBuilder.ApplyConfiguration(new PerfilConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());

        }
    }
}
