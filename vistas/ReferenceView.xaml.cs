using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class ReferenceView : ContentPage
{
	public ReferenceView()
	{
		InitializeComponent();
        if (BindingContext is ReferenciasViewModel viewModel)
        {
            viewModel.LoadReferenciasByUserCommand.Execute(null);
        }
    }
    private async void volver(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePacient");
    }
}