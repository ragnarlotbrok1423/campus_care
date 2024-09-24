namespace campusCare.vistas;

public partial class HomePacient : ContentPage
{
    public HomePacient()
    {
        InitializeComponent();
    }

    private async void AgendarCita(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AgendarCitas");
    }
}