namespace campusCare.vistas;
using campusCare.vistasModelos;
public partial class DoctorMainPage : ContentPage
{
	public DoctorMainPage()
	{
		InitializeComponent();
       BindingContext = new UsuariosViewModel();
	}
    private async void DonarSangre (object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///DonantesView");
	}
    private async void Receta (object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CrearRecetasView");

    }
    private async void Medicamentos (object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///DrogsView");
    }
    private async void citas (object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///DrogsView");
    }

}