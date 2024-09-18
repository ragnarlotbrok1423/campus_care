using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class CRUDpacientes : ContentPage
{
	public CRUDpacientes()
	{
		InitializeComponent();
        if (BindingContext is UsuariosViewModel viewModel)
        {
            viewModel.LoadUsuariosCommand.Execute(null);
        }
    }

    private async void AddUserNavigation(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreateUser");

    }
}