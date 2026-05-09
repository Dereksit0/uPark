namespace UPark.Views;

public partial class EstacionamientoFavoritoPage : ContentPage
{
    private const string PrefKey = "estacionamiento_favorito";
    private string _seleccionActual = string.Empty;

    public EstacionamientoFavoritoPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _seleccionActual = Preferences.Get(PrefKey, string.Empty);
        ActualizarSeleccion(_seleccionActual);
    }

    private void ActualizarSeleccion(string favorito)
    {
        CheckIMM.IsVisible = favorito == "IMM";
        CheckUMAD.IsVisible = favorito == "UMAD";

        CardIMM.Stroke = favorito == "IMM"
            ? Color.FromArgb("#1565c0") : Color.FromArgb("#e0e0e0");
        CardIMM.StrokeThickness = favorito == "IMM" ? 2.5 : 1;

        CardUMAD.Stroke = favorito == "UMAD"
            ? Color.FromArgb("#1565c0") : Color.FromArgb("#e0e0e0");
        CardUMAD.StrokeThickness = favorito == "UMAD" ? 2.5 : 1;
    }

    private void OnSeleccionarIMM(object sender, TappedEventArgs e)
    {
        _seleccionActual = "IMM";
        ActualizarSeleccion(_seleccionActual);
    }

    private void OnSeleccionarUMAD(object sender, TappedEventArgs e)
    {
        _seleccionActual = "UMAD";
        ActualizarSeleccion(_seleccionActual);
    }

    private void OnQuitarFavorito(object sender, TappedEventArgs e)
    {
        _seleccionActual = string.Empty;
        ActualizarSeleccion(_seleccionActual);
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_seleccionActual))
            Preferences.Remove(PrefKey);
        else
            Preferences.Set(PrefKey, _seleccionActual);

        await Shell.Current.GoToAsync("//MiCuentaPage");
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}
