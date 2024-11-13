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
    public partial class DonantesViewModel : ObservableObject
    {

        public readonly HttpClient _httpClient;


        [ObservableProperty]
        private ObservableCollection<DonantesDTO> donantes = new ObservableCollection<DonantesDTO>();

        public IAsyncRelayCommand LoadDonantesCommand { get; }


        public DonantesViewModel()
        {
            ServerString server = new ServerString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(server.cabecera)
            };
            LoadDonantesCommand = new AsyncRelayCommand(LoadDonantesAsync);

        }

        public async Task LoadDonantesAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("api/DonantesSangres");
                response.EnsureSuccessStatusCode();

                var jsonString = response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta de la api de donantes: {jsonString}");

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<DonantesDTO>>();
                Debug.WriteLine($"La conversion de la api de donantes es: {result}");

                if (result != null && result.Values != null)
                {
                    Donantes.Clear();
                    foreach (var donantes in result.Values)
                    {
                        Donantes.Add(donantes);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar a los donantes : {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    }
}
