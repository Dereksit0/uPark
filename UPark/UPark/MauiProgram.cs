using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using UPark.Data.Context;
using UPark.Data.Models;
using UPark.Services;

namespace UPark
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Base de datos SQLite en el directorio de la app
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "upark.db");
            builder.Services.AddDbContext<UParkDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // Servicios
            builder.Services.AddTransient<IUsuarioService, UsuarioService>();
            builder.Services.AddTransient<IReservaService, ReservaService>();

            // Páginas que usan inyección de dependencias
            builder.Services.AddTransient<Views.LoginPage>();
            builder.Services.AddTransient<Views.RegistroPage>();
            builder.Services.AddTransient<Views.DetalleEspacioPage>();
            builder.Services.AddTransient<Views.ReservaPage>();
            builder.Services.AddTransient<Views.MisReservasPage>();
            builder.Services.AddTransient<Views.CambiarContrasenaPage>();
            builder.Services.AddTransient<Views.EstacionamientoFavoritoPage>();
            builder.Services.AddTransient<Views.NotificacionesPreferenciasPage>();
            builder.Services.AddTransient<Views.OlvidasteContrasenaPage>();

            var app = builder.Build();

            // Inicializar la base de datos
            InicializarBaseDatos(app.Services);

            return app;
        }

        private static void InicializarBaseDatos(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UParkDbContext>();

            // Crear tablas si no existen (preserva datos existentes)
            context.Database.EnsureCreated();

            // Crear tabla Reservas si no existe (para bases de datos ya creadas)
            context.Database.ExecuteSqlRaw(@"
                CREATE TABLE IF NOT EXISTS Reservas (
                    IdReserva INTEGER NOT NULL CONSTRAINT PK_Reservas PRIMARY KEY AUTOINCREMENT,
                    Matricula TEXT NOT NULL,
                    IdEspacio INTEGER NOT NULL,
                    FechaHora TEXT NOT NULL,
                    Estado TEXT NOT NULL,
                    CONSTRAINT FK_Reservas_Usuarios FOREIGN KEY (Matricula) REFERENCES Usuarios(Matricula),
                    CONSTRAINT FK_Reservas_Espacios FOREIGN KEY (IdEspacio) REFERENCES Espacios(IdEspacio)
                )");

            // Sembrar espacios si no existen
            if (!context.Espacios.Any())
            {
                // 20 espacios para IMM (IdEstacionamiento = 1)
                for (int i = 1; i <= 20; i++)
                    context.Espacios.Add(new Espacio { Numero = i, IdEstacionamiento = 1, Estado = "Libre" });

                // 20 espacios para UMAD (IdEstacionamiento = 2)
                for (int i = 1; i <= 20; i++)
                    context.Espacios.Add(new Espacio { Numero = i, IdEstacionamiento = 2, Estado = "Libre" });

                context.SaveChanges();
            }
        }
    }
}
