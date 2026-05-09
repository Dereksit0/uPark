using UPark.Data.Models;
using UPark.Services;

namespace UPark.Views;

public partial class MisReservasPage : ContentPage
{
    private readonly IReservaService _reservaService;
    private Reserva? _reservaActiva;

    public MisReservasPage(IReservaService reservaService)
    {
        InitializeComponent();
        _reservaService = reservaService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarReservaAsync();
    }

    private async Task CargarReservaAsync()
    {
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        NoReservaPanel.IsVisible = false;
        ReservaActivaPanel.IsVisible = false;

        string matricula = Preferences.Get("matricula", string.Empty);
        _reservaActiva = await _reservaService.GetReservaActivaAsync(matricula);

        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;

        if (_reservaActiva == null)
        {
            NoReservaPanel.IsVisible = true;
        }
        else
        {
            EstacionamientoLabel.Text = _reservaActiva.Espacio.Estacionamiento.Nombre;
            EspacioLabel.Text = _reservaActiva.Espacio.Numero.ToString();
            FechaLabel.Text = _reservaActiva.FechaHora.ToString("dd/MM/yyyy HH:mm");
            CancelarBtn.IsEnabled = true;
            ReservaActivaPanel.IsVisible = true;
        }

        RefreshControl.IsRefreshing = false;
    }

    private void OnCancelarReservaClicked(object sender, EventArgs e)
    {
        if (_reservaActiva == null) return;
        CancelarMensaje.Text = $"¿Deseas cancelar la reserva del espacio " +
                               $"{_reservaActiva.Espacio.Numero} en " +
                               $"{_reservaActiva.Espacio.Estacionamiento.Nombre}?\n\nEl espacio quedará libre.";
        CancelarOverlay.IsVisible = true;
    }

    private void OnCancelarOverlayCerrar(object sender, EventArgs e)
    {
        CancelarOverlay.IsVisible = false;
    }

    private async void OnCancelarOverlayConfirmar(object sender, EventArgs e)
    {
        CancelarOverlay.IsVisible = false;
        if (_reservaActiva == null) return;

        CancelarBtn.IsEnabled = false;
        bool resultado = await _reservaService.CancelarReservaAsync(_reservaActiva.IdReserva);

        if (resultado)
            await CargarReservaAsync();
        else
        {
            CancelarBtn.IsEnabled = true;
            await DisplayAlert("Error", "No se pudo cancelar la reserva. Intenta de nuevo.", "OK");
        }
    }

    private async void OnIrDisponibilidadClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await CargarReservaAsync();
    }
}
