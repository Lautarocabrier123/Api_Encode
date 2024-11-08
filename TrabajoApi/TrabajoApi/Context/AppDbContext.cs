using TrabajoApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace TrabajoApi.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Connection");
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>(tb =>
            {
                tb.HasKey(col => col.IdPerfil);
                tb.Property(col => col.IdPerfil).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
                tb.ToTable("Perfil");
                tb.HasData(
                    new Perfil { IdPerfil = 1, Nombre = "Programador Dev" },
                    new Perfil { IdPerfil = 2, Nombre = "Programador Senior" },
                    new Perfil { IdPerfil = 3, Nombre = "Analista" }
                );
            });

            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(col => col.IdEmpleado);
                tb.Property(col => col.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.NombreCompleto).HasMaxLength(50);
                tb.HasOne(col => col.PerfilReferencia).WithMany(p => p.EmpleadosReferencia)
                    .HasForeignKey(col => col.IdPerfil);
                tb.ToTable("Empleado");
            });
        }
    }
}
