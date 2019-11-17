using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JMusik.Models;

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

        public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-KMLCSOQ;Database=TiendaDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.ToTable("DetalleOrden", "tienda");

                entity.HasIndex(e => e.OrdenId);

                entity.HasIndex(e => e.ProductoId);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Orden)
                    .WithMany(p => p.DetalleOrden)
                    .HasForeignKey(d => d.OrdenId);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleOrden)
                    .HasForeignKey(d => d.ProductoId);
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("Orden", "tienda");

                entity.HasIndex(e => e.UsuarioId);

                entity.Property(e => e.CantidadArticulos).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.UsuarioId);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("Perfil", "tienda");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto", "tienda");

                entity.Property(e => e.Nombre).HasMaxLength(256);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario", "tienda");

                entity.HasIndex(e => e.PerfilId);

                entity.Property(e => e.Apellidos).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(512);

                entity.Property(e => e.Username).HasMaxLength(25);

                entity.HasOne(d => d.Perfil)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.PerfilId);
            });
        }
    }
}
