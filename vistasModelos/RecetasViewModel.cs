using campusCare.modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace campusCare.vistasModelos
{
    public partial class RecetasViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        [ObservableProperty]
        private ObservableCollection<RecetasDTO> recetas = new ObservableCollection<RecetasDTO>();

        public IAsyncRelayCommand LoadRecetasCommand { get; }

        public RecetasViewModel()
        {
            ServerString server = new ServerString();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            LoadRecetasCommand = new AsyncRelayCommand(LoadRecetasAsync);

        }
        public async Task LoadRecetasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Recetas");
                response.EnsureSuccessStatusCode();

                // leeemos el contenido del json

                var jsonString = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Respuesta de la api: {jsonString}");

                //deserializamos el json que hemos extraido

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<RecetasDTO>>();

                if (result != null && result.Values != null)
                {
                    Recetas.Clear();
                    foreach (var usuario in result.Values)
                    {
                        Recetas.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar las recetas : {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
