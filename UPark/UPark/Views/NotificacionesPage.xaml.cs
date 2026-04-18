namespace UPark.Views;

public partial class NotificacionesPage : ContentPage
{
    public NotificacionesPage()
    {
        InitializeComponent();
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        RefreshControl.IsRefreshing = false;
    }
}