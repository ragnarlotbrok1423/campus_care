using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class RecetasView : ContentPage
{
    public RecetasView()
    {
        InitializeComponent();
        BindingContext = new RecetasViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as RecetasViewModel;
        if (viewModel != null)
        {
            await viewModel.LoadRecetasCommand.ExecuteAsync(null);
        }
    }
}