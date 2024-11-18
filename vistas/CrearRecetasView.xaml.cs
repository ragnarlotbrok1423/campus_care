using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class CrearRecetasView : ContentPage
{
    private readonly CrearRecetasViewModel _viewModel;
    public CrearRecetasView()
    {
        InitializeComponent();
        _viewModel = new CrearRecetasViewModel(); // Inicializa _viewModel primero
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CargarCategoriasCommand.Execute(null);
        _viewModel.LoadPacientesCommand.Execute(null);


    }

    private void CategoriaSeleccionada(object? sender, EventArgs e)
    {
        _viewModel.OnCategoriaChanged();
    }
}