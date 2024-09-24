using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class HistoryPacient : ContentPage
{

    public HistoryPacient()
    {
        InitializeComponent();
        if (BindingContext is HistorialMedicoViewModel viewModel)
        {
            viewModel.LoadHistorialCommand.Execute(null);
        }

    }

    private async void home(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePacient");
    }
}