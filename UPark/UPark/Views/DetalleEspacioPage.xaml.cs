namespace UPark.Views;

[QueryProperty(nameof(Numero), "numero")]
[QueryProperty(nameof(Estado), "estado")]
public partial class DetalleEspacioPage : ContentPage
{
    private string _numero = "0";
    private string _estado = "Libre";

    public string Numero
    {
        get => _numero;
        set
        {
            _numero = value;
            NumeroLabel.Text = value;
        }
    }

    public string Estado
    {
        get => _estado;
        set
        {
            _estado = value;
            EstadoLabel.Text = value;
            EstadoLabel.TextColor = value == "Libre" ?
                Color.FromArgb("#4caf50") : Color.FromArgb("#f44336");
            IconoLabel.Text = value == "Libre" ? "🟢" : "🔴";
        }
    }

    public DetalleEspacioPage()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}