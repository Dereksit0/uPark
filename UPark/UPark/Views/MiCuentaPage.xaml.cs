namespace UPark.Views;

public partial class MiCuentaPage : ContentPage
{
    public MiCuentaPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string nombre = Preferences.Get("nombre", "Estudiante");
        string matricula = Preferences.Get("matricula", "000000");
        string favorito = Preferences.Get("estacionamiento_favorito", string.Empty);

        NombreLabel.Text = nombre;
        MatriculaLabel.Text = matricula;
        FavoritoLabel.Text = string.IsNullOrEmpty(favorito) ? "Sin favorito" : favorito;

        // Calcular iniciales
        var partes = nombre.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string iniciales = partes.Length >= 2
            ? $"{partes[0][0]}{partes[1][0]}"
            : nombre.Length > 0 ? nombre[0].ToString() : "?";
        InicialLabel.Text = iniciales.ToUpper();
    }

    private async void OnEstacionamientoFavoritoTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("EstacionamientoFavoritoPage");
    }

    private async void OnNotificacionesTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("NotificacionesPreferenciasPage");
    }

    private async void OnCambiarContrasenaTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("CambiarContrasenaPage");
    }

    private void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        LogoutOverlay.IsVisible = true;
    }

    private void OnLogoutCancelar(object sender, EventArgs e)
    {
        LogoutOverlay.IsVisible = false;
    }

    private async void OnLogoutConfirmar(object sender, EventArgs e)
    {
        LogoutOverlay.IsVisible = false;
        Preferences.Remove("matricula");
        Preferences.Remove("nombre");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
