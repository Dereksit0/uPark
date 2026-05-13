using UPark.Services;

namespace UPark.Views;

public partial class LoginPage : ContentPage
{
    private readonly IUsuarioService _usuarioService;
    private bool _contrasenaVisible = false;

    public LoginPage(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
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
        OjitoLabel.Text = _contrasenaVisible ? "OCULTAR" : "VER";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        bool hasError = false;
        LoginErrorPanel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(MatriculaEntry.Text))
        {
            MatriculaError.Text = "⚠ La matrícula no puede estar vacía";
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

        if (hasError) return;

        // Verificar si el usuario existe en la base de datos
        bool existe = await _usuarioService.ExisteMatriculaAsync(MatriculaEntry.Text);
        if (!existe)
        {
            MatriculaError.Text = "⚠ Este usuario no existe";
            MatriculaError.IsVisible = true;
            ContrasenaEntry.Text = string.Empty;
            MatriculaEntry.Focus();
            return;
        }

        // Validar contraseña
        var usuario = await _usuarioService.LoginAsync(MatriculaEntry.Text, ContrasenaEntry.Text);
        if (usuario == null)
        {
            LoginErrorLabel.Text = "⚠ Contraseña incorrecta";
            LoginErrorPanel.IsVisible = true;
            ContrasenaEntry.Text = string.Empty;
            ContrasenaEntry.Focus();
            return;
        }

        // Login exitoso
        Preferences.Set("matricula", usuario.Matricula);
        Preferences.Set("nombre", usuario.Nombre);
        await Shell.Current.GoToAsync("//HomePage");
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
