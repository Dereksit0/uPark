namespace UPark;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("MapaPage", typeof(Views.MapaPage));
        Routing.RegisterRoute("DetalleEspacioPage", typeof(Views.DetalleEspacioPage));
        Routing.RegisterRoute("RegistroPage", typeof(Views.RegistroPage));
        Routing.RegisterRoute("OlvidasteContrasenaPage", typeof(Views.OlvidasteContrasenaPage));
        Routing.RegisterRoute("ReservaPage", typeof(Views.ReservaPage));
        Routing.RegisterRoute("CambiarContrasenaPage", typeof(Views.CambiarContrasenaPage));
        Routing.RegisterRoute("EstacionamientoFavoritoPage", typeof(Views.EstacionamientoFavoritoPage));
        Routing.RegisterRoute("NotificacionesPreferenciasPage", typeof(Views.NotificacionesPreferenciasPage));
    }
}
