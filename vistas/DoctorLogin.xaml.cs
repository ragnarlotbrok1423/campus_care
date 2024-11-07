using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class DoctorLogin : ContentPage
{
    public DoctorLogin()
    {
        InitializeComponent();
        BindingContext = new DoctorLoginViewModel();
    }
    private bool togglePacientes = false;
    private async void pacientes_Clicked(object sender, EventArgs e)
    {
        if (togglePacientes)
        {
            pacientes.BackgroundColor = Colors.Transparent;
            pacientes.BorderColor = Color.FromHex("#136AED");
            pacientes.TextColor = Color.FromHex("#136AED");
            pacientes.BorderWidth = 3;
            await Shell.Current.GoToAsync("///Login");
        }
        else
        {
            pacientes.BackgroundColor = Color.FromHex("#136AED");
            pacientes.BorderColor = Colors.Transparent;
            pacientes.TextColor = Colors.White;
            pacientes.BorderWidth = 0;
        }
        togglePacientes = !togglePacientes;

    }
    private bool toggleMedicos = true;
    private async void doctores_Clicked(object sender, EventArgs e)
    {
        if (toggleMedicos)
        {
            doctores.BackgroundColor = Colors.Transparent;
            doctores.BorderColor = Color.FromHex("#136AED");
            doctores.TextColor = Color.FromHex("#136AED");
            doctores.BorderWidth = 3;

        }
        else
        {
            doctores.BackgroundColor = Color.FromHex("#136AED");
            doctores.BorderColor = Colors.Transparent;
            doctores.TextColor = Colors.White;
            doctores.BorderWidth = 0;

        }
        toggleMedicos = !toggleMedicos;
    }
}