using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
    private bool togglePacientes = true;
    private void pacientes_Clicked(object sender, EventArgs e)
    {
        if (togglePacientes)
        {
            pacientes.BackgroundColor = Colors.Transparent;
            pacientes.BorderColor = Color.FromHex("#136AED");
            pacientes.TextColor = Color.FromHex("#136AED");
            pacientes.BorderWidth = 3;
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
    private bool toggleMedicos = false;
    private async void doctores_Clicked(object sender, EventArgs e)
    {
        if (toggleMedicos)
        {
            doctores.BackgroundColor = Colors.Transparent;
            doctores.BorderColor = Color.FromHex("#136AED");
            doctores.TextColor = Color.FromHex("#136AED");
            doctores.BorderWidth = 3;
            await Shell.Current.GoToAsync("///DoctorLogin");
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