using Microsoft.EntityFrameworkCore;
using UPark.Data.Models;

namespace UPark.Data.Context;

public class UParkDbContext : DbContext
{
    public UParkDbContext(DbContextOptions<UParkDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Estacionamiento> Estacionamientos { get; set; }
    public DbSet<Espacio> Espacios { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Usuario - clave primaria string
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Matricula);

        // Estacionamiento - Espacios
        modelBuilder.Entity<Espacio>()
            .HasOne(e => e.Estacionamiento)
            .WithMany(est => est.Espacios)
            .HasForeignKey(e => e.IdEstacionamiento);

        // Usuario - Notificaciones
        modelBuilder.Entity<Notificacion>()
            .HasOne(n => n.Usuario)
            .WithMany(u => u.Notificaciones)
            .HasForeignKey(n => n.Matricula);

        // Seed data - datos de prueba
        modelBuilder.Entity<Estacionamiento>().HasData(
            new Estacionamiento { IdEstacionamiento = 1, Nombre = "IMM", Ubicacion = "Frente a la Universidad", CapacidadTotal = 50 },
            new Estacionamiento { IdEstacionamiento = 2, Nombre = "UMAD", Ubicacion = "Detrás de la Universidad", CapacidadTotal = 40 }
        );
    }
}