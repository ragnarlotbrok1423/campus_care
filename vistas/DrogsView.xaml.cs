using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class DrogsView : ContentPage
{
    public DrogsView()
    {
        InitializeComponent();
        BindingContext = new MedicamentosViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as MedicamentosViewModel;
        if (viewModel != null)
        {
            await viewModel.LoadMedicamentosCommand.ExecuteAsync(null);
        }
    }
}