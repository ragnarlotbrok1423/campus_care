using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using campusCare.modelos;
using campusCare.vistasModelos;

namespace campusCare.vistas;

public partial class descargarPDFTestview : ContentPage
{
    private readonly ReferenciasViewModel _viewModel;

    public descargarPDFTestview()
    {
        InitializeComponent();
        _viewModel = new ReferenciasViewModel();
        BindingContext = _viewModel;
        
    }


   

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as ReferenciasViewModel;
        if (viewModel != null)
        {
            await viewModel.LoadReferenciasCommand.ExecuteAsync(null);
            
        }
    }
}