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

        // Parsear la URI y obtener los parámetros de consulta manualmente
        var uri = new Uri(route);
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);

        if (queryParameters["id"] != null)
        {
            int idPaciente = int.Parse(queryParameters["id"]);
            await ((CreateUserViewModel)BindingContext).CargarPacienteParaEdicionAsync(idPaciente);  // Cargar datos del paciente para editar
        }
    }
    private async void Regresar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CRUDpaciente");

    }


}



