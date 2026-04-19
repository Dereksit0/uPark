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
        NombreLabel.Text = Preferences.Get("nombre", "Estudiante");
        MatriculaLabel.Text = Preferences.Get("matricula", "000000");
    }

    private async void OnEstacionamientoFavoritoTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Próximamente",
            "Esta función estará disponible en la siguiente versión.", "OK");
    }

    private async void OnNotificacionesTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Próximamente",
            "Esta función estará disponible en la siguiente versión.", "OK");
    }

    private async void OnCambiarContrasenaTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Próximamente",
            "Esta función estará disponible en la siguiente versión.", "OK");
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Cerrar Sesión",
            "¿Estás seguro que deseas cerrar sesión?",
            "Sí",
            "No");

        if (confirm)
        {
            Preferences.Remove("matricula");
            Preferences.Remove("nombre");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}