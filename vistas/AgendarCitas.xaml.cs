using campusCare.modelos;
using campusCare.vistasModelos;
using campusCare.vistas;
namespace campusCare.vistas;

public partial class AgendarCitas : ContentPage
{
    private readonly CrearCitaViewModel _viewModel;
    public AgendarCitas()
    {
        InitializeComponent();
        _viewModel = new CrearCitaViewModel();
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.CargarEspecialidadesCommand.Execute(null);
        _viewModel.CargarTiposDeCitaCommand.Execute(null);
    }


    private void picker2_SelectedIndexChanged(object sender, EventArgs e)
    {

        _viewModel.OnEspecialidadChanged();

    }
}