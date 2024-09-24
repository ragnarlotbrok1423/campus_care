using campusCare.modelos;
using campusCare.vistasModelos;
using campusCare.vistas;
namespace campusCare.vistas;

public partial class AgendarCitas : ContentPage
{
    public AgendarCitas()
    {
        InitializeComponent();

    }

    private static void OnGuardarInfoClicked(object sender, EventArgs e)
    {
        CitaMedicaService citaMedicaService = new CitaMedicaService();

        CitaRequest cita = new CitaRequest
        {
            fecha = "2024-05-12",
            certificadoBuenaSalud = 0,
            peso = 0,
            inhaloterapias = 0,
            inyecciones = "",
            glisemiaCapilar = 0,
            referenciaMedica = "tiene miopia avanzada, requiere oftamologo",
            tipoConsulta = "medicinaGeneral",
            idUsuario = 2,
            idDoctor = 1
        }
        ;
        citaMedicaService.CreateAppointmentAsync(cita);
    }

    private async void home(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePacient");
    }
}