using UPark.Services;

namespace UPark.Views;

public partial class RegistroPage : ContentPage
{
    private readonly IUsuarioService _usuarioService;
    private bool _contrasenaVisible = false;
    private bool _confirmarVisible = false;

    public RegistroPage(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private void OnToggleContrasena(object sender, EventArgs e)
    {
        _contrasenaVisible = !_contrasenaVisible;
        ContrasenaEntry.IsPassword = !_contrasenaVisible;
        ToggleContrasenaLabel.Text = _contrasenaVisible ? "OCULTAR" : "VER";
    }

    private void OnToggleConfirmar(object sender, EventArgs e)
    {
        _confirmarVisible = !_confirmarVisible;
        ConfirmarContrasenaEntry.IsPassword = !_confirmarVisible;
        ToggleConfirmarLabel.Text = _confirmarVisible ? "OCULTAR" : "VER";
    }

    private async void OnCrearCuentaClicked(object sender, EventArgs e)
    {
        bool hasError = false;
        RegistroErrorPanel.IsVisible = false;

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

        if (hasError) return;

        // Guardar en la base de datos
        var (success, error) = await _usuarioService.RegistrarAsync(
            NombreEntry.Text.Trim(),
            MatriculaEntry.Text.Trim(),
            CorreoEntry.Text.Trim(),
            ContrasenaEntry.Text);

        if (!success)
        {
            RegistroErrorLabel.Text = $"⚠ {error}";
            RegistroErrorPanel.IsVisible = true;
            return;
        }

        await DisplayAlert("¡Cuenta creada!", "Te has registrado correctamente. Ya puedes iniciar sesión.", "OK");
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private async void OnVolverLoginClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}
