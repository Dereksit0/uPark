using UPark.Services;

namespace UPark.Views;

public partial class OlvidasteContrasenaPage : ContentPage
{
    private readonly IUsuarioService _usuarioService;
    private bool _nuevaVisible = false;
    private string _correoVerificado = string.Empty;

    public OlvidasteContrasenaPage(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private void OnToggleNueva(object sender, EventArgs e)
    {
        _nuevaVisible = !_nuevaVisible;
        NuevaContrasenaEntry.IsPassword = !_nuevaVisible;
        ToggleNuevaLabel.Text = _nuevaVisible ? "OCULTAR" : "VER";
    }

    private async void OnVerificarClicked(object sender, EventArgs e)
    {
        CorreoError.IsVisible = false;

        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            CorreoError.Text = "⚠ El correo no puede estar vacío";
            CorreoError.IsVisible = true;
            return;
        }

        if (!CorreoEntry.Text.Contains('@') || !CorreoEntry.Text.Contains('.'))
        {
            CorreoError.Text = "⚠ Ingresa un correo válido";
            CorreoError.IsVisible = true;
            return;
        }

        VerificarBtn.IsEnabled = false;

        bool existe = await _usuarioService.CorreoExisteAsync(CorreoEntry.Text.Trim());

        VerificarBtn.IsEnabled = true;

        if (!existe)
        {
            CorreoError.Text = "⚠ No encontramos ninguna cuenta con ese correo";
            CorreoError.IsVisible = true;
            return;
        }

        _correoVerificado = CorreoEntry.Text.Trim();
        CorreoConfirmadoLabel.Text = $"Cuenta verificada: {_correoVerificado}";
        Paso1Panel.IsVisible = false;
        Paso2Panel.IsVisible = true;
    }

    private async void OnCambiarClicked(object sender, EventArgs e)
    {
        NuevaError.IsVisible = false;

        if (string.IsNullOrWhiteSpace(NuevaContrasenaEntry.Text))
        {
            NuevaError.Text = "⚠ Ingresa la nueva contraseña";
            NuevaError.IsVisible = true;
            return;
        }

        if (NuevaContrasenaEntry.Text.Length < 6)
        {
            NuevaError.Text = "⚠ La contraseña debe tener al menos 6 caracteres";
            NuevaError.IsVisible = true;
            return;
        }

        if (NuevaContrasenaEntry.Text != ConfirmarContrasenaEntry.Text)
        {
            NuevaError.Text = "⚠ Las contraseñas no coinciden";
            NuevaError.IsVisible = true;
            return;
        }

        bool resultado = await _usuarioService.RestablecerContrasenaAsync(
            _correoVerificado, NuevaContrasenaEntry.Text);

        if (resultado)
        {
            await DisplayAlert(
                "¡Contraseña actualizada!",
                "Tu contraseña ha sido cambiada correctamente. Ya puedes iniciar sesión.",
                "Ir al inicio de sesión");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            NuevaError.Text = "⚠ Ocurrió un error. Intenta de nuevo.";
            NuevaError.IsVisible = true;
        }
    }
}
