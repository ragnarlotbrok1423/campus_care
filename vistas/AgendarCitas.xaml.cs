using campusCare.modelos;
using campusCare.vistasModelos;
using campusCare.vistas;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Calendar;
using System.Net.Sockets;
namespace campusCare.vistas;


public partial class AgendarCitas : ContentPage
{
    private readonly CrearCitaViewModel _viewModel;
    public AgendarCitas()
    {
        InitializeComponent();
        _viewModel = new CrearCitaViewModel();
        BindingContext = _viewModel;
        this.calendar.SelectionMode = CalendarSelectionMode.Single;
        
        calendar.MonthView = new CalendarMonthView
        {
            TextStyle = new CalendarTextStyle
            {
                TextColor= Colors.White,
                FontFamily= "Ubuntu",                              
            }
            
        };
        calendar.HeaderView = new CalendarHeaderView
        {
            TextStyle = new CalendarTextStyle
            {
                TextColor = Colors.White,
                FontSize=12,
                FontFamily = "Ubuntu",
            }
        };
        
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

    private async void volver (object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePacient");
    }
}