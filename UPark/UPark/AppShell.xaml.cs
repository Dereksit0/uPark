namespace UPark;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("MapaPage", typeof(Views.MapaPage));
        Routing.RegisterRoute("DetalleEspacioPage", typeof(Views.DetalleEspacioPage));
    }
}