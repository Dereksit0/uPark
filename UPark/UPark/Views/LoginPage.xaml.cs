namespace UPark.Views;

public partial class LoginPage : ContentPage
{
    private bool _contrasenaVisible = false;

    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnMatriculaTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            string soloNumeros = new string(e.NewTextValue.Where(char.IsDigit).ToArray());
            if (e.NewTextValue != soloNumeros)
                MatriculaEntry.Text = soloNumeros;
        }
    }

    private void OnOjitoTapped(object sender, EventArgs e)
    {
        _contrasenaVisible = !_contrasenaVisible;
        ContrasenaEntry.IsPassword = !_contrasenaVisible;
        OjitoLabel.Text = _contrasenaVisible ? "🙈" : "👁";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        bool hasError = false;

        if (string.IsNullOrWhiteSpace(MatriculaEntry.Text))
        {
            MatriculaError.IsVisible = true;
            hasError = true;
        }
        else { MatriculaError.IsVisible = false; }

        if (string.IsNullOrWhiteSpace(ContrasenaEntry.Text))
        {
            ContrasenaError.IsVisible = true;
            hasError = true;
        }
        else { ContrasenaError.IsVisible = false; }

        if (!hasError)
        {
            Preferences.Set("matricula", MatriculaEntry.Text);
            Preferences.Set("nombre", "Carlos López");
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