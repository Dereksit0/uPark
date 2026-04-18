namespace UPark.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        bool hasError = false;

        // Validación matrícula
        if (string.IsNullOrWhiteSpace(MatriculaEntry.Text))
        {
            MatriculaError.IsVisible = true;
            hasError = true;
        }
        else
        {
            MatriculaError.IsVisible = false;
        }

        // Validación contraseña
        if (string.IsNullOrWhiteSpace(ContrasenaEntry.Text))
        {
            ContrasenaError.IsVisible = true;
            hasError = true;
        }
        else
        {
            ContrasenaError.IsVisible = false;
        }

        if (!hasError)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}