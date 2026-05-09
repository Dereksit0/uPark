using UPark.Services;

namespace UPark.Views;

public partial class CambiarContrasenaPage : ContentPage
{
    private readonly IUsuarioService _usuarioService;
    private bool _actualVisible = false;
    private bool _nuevaVisible = false;

    public CambiarContrasenaPage(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private void OnToggleActual(object sender, EventArgs e)
    {
        _actualVisible = !_actualVisible;
        ContrasenaActualEntry.IsPassword = !_actualVisible;
        ToggleActualLabel.Text = _actualVisible ? "OCULTAR" : "VER";
    }

    private void OnToggleNueva(object sender, EventArgs e)
    {
        _nuevaVisible = !_nuevaVisible;
        NuevaContrasenaEntry.IsPassword = !_nuevaVisible;
        ToggleNuevaLabel.Text = _nuevaVisible ? "OCULTAR" : "VER";
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        ErrorPanel.IsVisible = false;
        SuccessBanner.IsVisible = false;
        ActualError.IsVisible = false;
        NuevaError.IsVisible = false;

        bool hasError = false;

        if (string.IsNullOrWhiteSpace(ContrasenaActualEntry.Text))
        {
            ActualError.Text = "⚠ Ingresa tu contraseña actual";
            ActualError.IsVisible = true;
            hasError = true;
        }

        if (string.IsNullOrWhiteSpace(NuevaContrasenaEntry.Text))
        {
            NuevaError.Text = "⚠ Ingresa la nueva contraseña";
            NuevaError.IsVisible = true;
            hasError = true;
        }
        else if (NuevaContrasenaEntry.Text.Length < 6)
        {
            NuevaError.Text = "⚠ La contraseña debe tener al menos 6 caracteres";
            NuevaError.IsVisible = true;
            hasError = true;
        }
        else if (NuevaContrasenaEntry.Text != ConfirmarNuevaEntry.Text)
        {
            NuevaError.Text = "⚠ Las contraseñas no coinciden";
            NuevaError.IsVisible = true;
            hasError = true;
        }

        if (hasError) return;

        GuardarBtn.IsEnabled = false;
        string matricula = Preferences.Get("matricula", string.Empty);

        bool resultado = await _usuarioService.CambiarContrasenaAsync(
            matricula,
            ContrasenaActualEntry.Text,
            NuevaContrasenaEntry.Text);

        GuardarBtn.IsEnabled = true;

        if (resultado)
        {
            ContrasenaActualEntry.Text = string.Empty;
            NuevaContrasenaEntry.Text = string.Empty;
            ConfirmarNuevaEntry.Text = string.Empty;
            SuccessBanner.IsVisible = true;
        }
        else
        {
            ErrorLabel.Text = "⚠ La contraseña actual es incorrecta. Intenta de nuevo.";
            ErrorPanel.IsVisible = true;
        }
    }
}
