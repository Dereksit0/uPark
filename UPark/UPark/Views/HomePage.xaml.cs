namespace UPark.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnIMMClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"MapaPage?estacionamiento=IMM");
    }

    private async void OnUMADClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"MapaPage?estacionamiento=UMAD");
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        RefreshControl.IsRefreshing = false;
    }
}