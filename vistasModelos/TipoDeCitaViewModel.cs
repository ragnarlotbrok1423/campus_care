using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace campusCare.vistasModelos;

public partial class TipoDeCitaViewModel: ObservableObject
{
    private readonly HttpClient _httpClient;

    [ObservableProperty] 
    private ObservableCollection<CitaRequest> cita = new ObservableCollection<CitaRequest>();

    [ObservableProperty]
    private ObservableCollection<TiposConsultas> tipoCita = new ObservableCollection<TiposConsultas>();

    [ObservableProperty] 
    private TiposConsultas tipoCitaSeleccionada;
    
    public IAsyncRelayCommand LoadCitaByTipoDeCita { get; }
    
    
    //constructor
    public TipoDeCitaViewModel()
    {
        ServerString server = new ServerString();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(server.cabecera)
        };
        LoadCitaByTipoDeCita = new AsyncRelayCommand(LoadCitaByTipoDeCitaAsync);
        TipoCita = new ObservableCollection<TiposConsultas>();
    }
    
    // Constructor para pruebas
    public TipoDeCitaViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
        LoadCitaByTipoDeCita = new AsyncRelayCommand(LoadCitaByTipoDeCitaAsync);
        TipoCita = new ObservableCollection<TiposConsultas>();
    }

    // declaracion de funciones
    public async Task LoadCitaByTipoDeCitaAsync()
    {
        try
        {
            var result = await _httpClient.
                GetAsync($"api/CitasMedicas/tipoCita/{tipoCitaSeleccionada.IdtiposConsultas}");
            var jsonString = await result.Content.ReadAsStringAsync();
            Debug.WriteLine($"La respuesta de la api es: {jsonString}");
            
            //Deserializar el json de la api 

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<TiposConsultas>>();

            if (response != null && response.Values != null)
            {
                TipoCita.Clear();

                foreach (var cita in response.Values)
                {
                    TipoCita.Add(cita);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al cargar los tipos de consulta: {ex.Message}");
            Debug.WriteLine($"StackTrace: {ex.StackTrace}");
        }
    }
}