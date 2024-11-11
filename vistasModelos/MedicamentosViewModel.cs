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
    public partial class MedicamentosViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        [ObservableProperty]
        private ObservableCollection<MedicamentosDTO> medicamentos = new ObservableCollection<MedicamentosDTO>();

        public IAsyncRelayCommand LoadMedicamentosCommand { get; }

        public MedicamentosViewModel()
        {
            ServerString server = new ServerString();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            LoadMedicamentosCommand = new AsyncRelayCommand(LoadMedicamentosAsync);

        }

        public async Task LoadMedicamentosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Medicamentos");
                response.EnsureSuccessStatusCode();

                // leeemos el contenido del json

                var jsonString = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Respuesta de la api: {jsonString}");

                //deserializamos el json que hemos extraido

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<MedicamentosDTO>>();

                if (result != null && result.Values != null)
                {
                    Medicamentos.Clear();
                    foreach (var usuario in result.Values)
                    {
                        Medicamentos.Add(usuario);
                    }
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar Medicamentos : {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    }
}
