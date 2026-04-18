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

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        RefreshControl.IsRefreshing = false;
    }
}