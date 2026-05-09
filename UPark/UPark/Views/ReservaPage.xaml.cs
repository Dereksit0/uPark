using UPark.Services;

namespace UPark.Views;

[QueryProperty(nameof(Numero), "numero")]
[QueryProperty(nameof(Estacionamiento), "estacionamiento")]
public partial class ReservaPage : ContentPage
{
    private readonly IReservaService _reservaService;
    private int _idEspacio;
    private string _numero = string.Empty;
    private string _estacionamiento = string.Empty;

    public string Numero
    {
        get => _numero;
        set { _numero = Uri.UnescapeDataString(value); NumeroLabel.Text = _numero; }
    }

    public string Estacionamiento
    {
        get => _estacionamiento;
        set { _estacionamiento = Uri.UnescapeDataString(value); EstacionamientoLabel.Text = _estacionamiento; }
    }

    public ReservaPage(IReservaService reservaService)
    {
        InitializeComponent();
        _reservaService = reservaService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarEspacioAsync();
    }

    private async Task CargarEspacioAsync()
    {
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        IconoPanel.IsVisible = false;
        DetallePanel.IsVisible = false;
        ConfirmarBtn.IsVisible = false;
        ErrorPanel.IsVisible = false;

        if (string.IsNullOrEmpty(_numero) || string.IsNullOrEmpty(_estacionamiento)
            || !int.TryParse(_numero, out int num))
        {
            MostrarError("Información del espacio no válida.");
            return;
        }

        var espacio = await _reservaService.GetEspacioAsync(num, _estacionamiento);

        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;
        IconoPanel.IsVisible = true;
        DetallePanel.IsVisible = true;

        if (espacio == null)
        {
            MostrarError("El espacio no fue encontrado en la base de datos.");
            return;
        }

        _idEspacio = espacio.IdEspacio;
        EstadoLabel.Text = espacio.Estado;
        EstadoLabel.TextColor = espacio.Estado == "Libre"
            ? Color.FromArgb("#4caf50")
            : Color.FromArgb("#f44336");

        if (espacio.Estado == "Libre")
            ConfirmarBtn.IsVisible = true;
        else
            MostrarError("El espacio ya no está disponible. Por favor elige otro.");
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        string matricula = Preferences.Get("matricula", string.Empty);
        if (string.IsNullOrEmpty(matricula))
        {
            MostrarError("Sesión no válida. Por favor inicia sesión nuevamente.");
            return;
        }

        ConfirmarBtn.IsEnabled = false;
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        var (success, error) = await _reservaService.ReservarEspacioAsync(matricula, _idEspacio);

        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;

        if (success)
        {
            await DisplayAlert("¡Reserva exitosa!",
                $"Has reservado el espacio {_numero} en el estacionamiento {_estacionamiento}.", "OK");
            await Shell.Current.GoToAsync("//MisReservasPage");
        }
        else
        {
            ConfirmarBtn.IsEnabled = true;
            MostrarError(error);
        }
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private async void OnCancelarClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private void MostrarError(string mensaje)
    {
        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;
        ErrorLabel.Text = mensaje;
        ErrorPanel.IsVisible = true;
    }
}
