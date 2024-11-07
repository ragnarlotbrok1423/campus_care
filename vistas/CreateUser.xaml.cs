using campusCare.vistasModelos;
using System.Web;

namespace campusCare.vistas;

public partial class CreateUser : ContentPage
{
    public CreateUser()
    {
        InitializeComponent();
        BindingContext = new CreateUserViewModel();

    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Obtener la ruta completa desde la URI
        var route = Shell.Current.CurrentState.Location.ToString();


    }
    private async void Regresar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CRUDpaciente");

    }


}



