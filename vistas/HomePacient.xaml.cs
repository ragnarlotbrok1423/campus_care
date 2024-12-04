namespace campusCare.vistas;
using campusCare.vistasModelos;

public partial class HomePacient : ContentPage
{
    public HomePacient()
    {
        InitializeComponent();
        BindingContext = new HomePacientsViewModel();
    }

    private async void AgendarCita(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AgendarCitas");
    }
}