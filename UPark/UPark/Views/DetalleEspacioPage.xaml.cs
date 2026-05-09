using UPark.Services;

namespace UPark.Views;

[QueryProperty(nameof(Numero), "numero")]
[QueryProperty(nameof(Estado), "estado")]
[QueryProperty(nameof(Estacionamiento), "estacionamiento")]
public partial class DetalleEspacioPage : ContentPage
{
    private readonly IReservaService _reservaService;
    private string _numero = "0";
    private string _estado = "Libre";
    private string _estacionamiento = string.Empty;

    public string Numero
    {
        get => _numero;
        set { _numero = value; NumeroLabel.Text = value; }
    }

    public string Estado
    {
        get => _estado;
        set { _estado = value; }
    }

    public string Estacionamiento
    {
        get => _estacionamiento;
        set { _estacionamiento = Uri.UnescapeDataString(value); EstacionamientoLabel.Text = _estacionamiento; }
    }

    public DetalleEspacioPage(IReservaService reservaService)
    {
        InitializeComponent();
        _reservaService = reservaService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarEstadoRealAsync();
    }

    private async Task CargarEstadoRealAsync()
    {
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        IconoPanel.IsVisible = false;
        DetallePanel.IsVisible = false;
        ReservarBtn.IsVisible = false;
        OcupadoPanel.IsVisible = false;

        if (!string.IsNullOrEmpty(_numero) && !string.IsNullOrEmpty(_estacionamiento)
            && int.TryParse(_numero, out int num))
        {
            var espacio = await _reservaService.GetEspacioAsync(num, _estacionamiento);
            if (espacio != null)
                _estado = espacio.Estado;
        }

        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;

        // Actualizar UI con el estado real
        ActualizacionLabel.Text = DateTime.Now.ToString("HH:mm");
        EstadoLabel.Text = _estado;
        EstadoLabel.TextColor = _estado == "Libre"
            ? Color.FromArgb("#4caf50")
            : Color.FromArgb("#f44336");
        IconoLabel.Text = _estado == "Libre" ? "🟢" : "🔴";

        IconoPanel.IsVisible = true;
        DetallePanel.IsVisible = true;

        if (_estado == "Libre")
            ReservarBtn.IsVisible = true;
        else
            OcupadoPanel.IsVisible = true;
    }

    private async void OnReservarClicked(object sender, EventArgs e)
    {
        string num = Uri.EscapeDataString(_numero);
        string est = Uri.EscapeDataString(_estacionamiento);
        await Shell.Current.GoToAsync($"ReservaPage?numero={num}&estacionamiento={est}");
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
