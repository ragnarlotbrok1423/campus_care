using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}