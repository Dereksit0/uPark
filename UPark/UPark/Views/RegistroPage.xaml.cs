namespace UPark.Views;

public partial class RegistroPage : ContentPage
{
    public RegistroPage()
    {
        InitializeComponent();
    }

    private async void OnCrearCuentaClicked(object sender, EventArgs e)
    {
        bool hasError = false;

        if (string.IsNullOrWhiteSpace(NombreEntry.Text))
        { NombreError.IsVisible = true; hasError = true; }
        else { NombreError.IsVisible = false; }

        if (string.IsNullOrWhiteSpace(MatriculaEntry.Text))
        { MatriculaError.IsVisible = true; hasError = true; }
        else { MatriculaError.IsVisible = false; }

        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        { CorreoError.IsVisible = true; hasError = true; }
        else { CorreoError.IsVisible = false; }

        if (string.IsNullOrWhiteSpace(ContrasenaEntry.Text))
        { ContrasenaError.IsVisible = true; hasError = true; }
        else { ContrasenaError.IsVisible = false; }

        if (ContrasenaEntry.Text != ConfirmarContrasenaEntry.Text)
        { ConfirmarError.IsVisible = true; hasError = true; }
        else { ConfirmarError.IsVisible = false; }

        if (!hasError)
        {
            await DisplayAlert("¡Éxito!", "Cuenta creada correctamente.", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    private async void OnVolverLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}