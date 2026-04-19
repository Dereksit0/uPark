namespace UPark.Views;

public partial class OlvidasteContrasenaPage : ContentPage
{
    public OlvidasteContrasenaPage()
    {
        InitializeComponent();
    }

    private async void OnEnviarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            CorreoError.IsVisible = true;
            return;
        }

        CorreoError.IsVisible = false;
        await DisplayAlert(
            "Correo enviado",
            $"Se enviaron instrucciones a {CorreoEntry.Text}",
            "OK");
        await Shell.Current.GoToAsync("..");
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}