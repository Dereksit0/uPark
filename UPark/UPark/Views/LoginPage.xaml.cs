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

        if (string.IsNullOrWhiteSpace(MatriculaEntry.Text))
        {
            MatriculaError.IsVisible = true;
            hasError = true;
        }
        else
        {
            MatriculaError.IsVisible = false;
        }

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

    private async void OnOlvidasteClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("OlvidasteContrasenaPage");
    }

    private async void OnRegistrarseClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegistroPage");
    }
}