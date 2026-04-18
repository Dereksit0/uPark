namespace UPark.Views;

public partial class MiCuentaPage : ContentPage
{
    public MiCuentaPage()
    {
        InitializeComponent();
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
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}