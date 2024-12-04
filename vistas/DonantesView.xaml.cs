using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class DonantesView : ContentPage
{
    public DonantesView()
    {
        InitializeComponent();
        BindingContext = new DonantesViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as DonantesViewModel;
        if (viewModel != null)
        {
            await viewModel.LoadDonantesCommand.ExecuteAsync(null);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///DoctorMainPage");
    }
}