namespace UPark.Views;

[QueryProperty(nameof(Estacionamiento), "estacionamiento")]
public partial class MapaPage : ContentPage
{
    private string _estacionamiento = "IMM";

    public string Estacionamiento
    {
        get => _estacionamiento;
        set
        {
            _estacionamiento = value;
            TituloEstacionamiento.Text = $"{value} Parking";
        }
    }

    public MapaPage()
    {
        InitializeComponent();
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    private async void OnEspacioTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is string param)
        {
            var parts = param.Split(',');
            string numero = parts[0];
            string estado = parts[1];
            string est = Uri.EscapeDataString(_estacionamiento);
            await Shell.Current.GoToAsync($"DetalleEspacioPage?numero={numero}&estado={estado}&estacionamiento={est}");
        }
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        RefreshControl.IsRefreshing = false;
    }
}