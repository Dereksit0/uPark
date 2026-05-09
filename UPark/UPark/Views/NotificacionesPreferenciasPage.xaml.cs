namespace UPark.Views;

public partial class NotificacionesPreferenciasPage : ContentPage
{
    public NotificacionesPreferenciasPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SwitchEspacios.IsToggled = Preferences.Get("notif_espacios", true);
        SwitchCasiLleno.IsToggled = Preferences.Get("notif_casi_lleno", true);
        SwitchReserva.IsToggled = Preferences.Get("notif_reserva", true);
        SwitchRecordatorio.IsToggled = Preferences.Get("notif_recordatorio", false);
        SwitchActualizaciones.IsToggled = Preferences.Get("notif_actualizaciones", true);
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        Preferences.Set("notif_espacios", SwitchEspacios.IsToggled);
        Preferences.Set("notif_casi_lleno", SwitchCasiLleno.IsToggled);
        Preferences.Set("notif_reserva", SwitchReserva.IsToggled);
        Preferences.Set("notif_recordatorio", SwitchRecordatorio.IsToggled);
        Preferences.Set("notif_actualizaciones", SwitchActualizaciones.IsToggled);

        await Shell.Current.GoToAsync("//MiCuentaPage");
    }

    private async void OnNavVolverClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}
